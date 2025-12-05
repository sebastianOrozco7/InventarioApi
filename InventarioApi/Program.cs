using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using InventarioApi.Data;
using InventarioApi.Services;
using Microsoft.OpenApi.Models;
using AutoMapper;
using InventarioApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

// ======================================================================
// 1. CONFIGURACIÓN DE SERVICIOS PARA LA APLICACIÓN (Dependency Injection)
// ======================================================================


// -------------------------------
// 1.1 Configurar conexión a MySQL
// -------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 23))
    )
);


// ------------------------------
// 1.2 Configurar Identity Core
// ------------------------------
// IdentityCore = sistema de usuarios mínimo (sin roles, sin cookies)
builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;     // Evita usuarios duplicados con el mismo email
})
.AddEntityFrameworkStores<AppDbContext>();       // Identity usará nuestras tablas en AppDbContext


// ------------------------------
// 1.3 Leer configuración del JWT
// ------------------------------
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];


// ------------------------------
// 1.4 Configurar autenticación JWT
// ------------------------------
// Aquí le decimos a la API que el sistema de autenticación será JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;   // Cómo se valida cada request
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;      // Qué hacer cuando NO hay token
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,   // Verifica quién emitió el token
        ValidateAudience = false, // No usas "audience", así que se desactiva
        ValidateLifetime = true, // Valida la expiración del token
        ValidateIssuerSigningKey = true, // Valida la firma del token
        ValidIssuer = jwtIssuer, // "Issuer" debe coincidir con el del token
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwtKey) // Firma secreta del token
        )
    };
});


// --------------------------------
// 1.5 Registro de servicios personalizados
// --------------------------------
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProvedorService, ProvedorService>();
builder.Services.AddScoped<IMovmientoService, MovimientoService>();



// --------------------------------
// 1.6 Registro de AutoMapper
// --------------------------------
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// --------------------------------
// 1.7 Controladores para la API
// --------------------------------
builder.Services.AddControllers();


// --------------------------------
// 1.8 Swagger + Configuración JWT
// --------------------------------
// Swagger permite probar la API desde el navegador
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InventarioApi",
        Version = "v1"
    });

    // Configuración para poder ingresar un JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa: Bearer {tu_token}"
    });

    // Swagger requiere que una API especifique qué esquema usa
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




// ======================================================================
// 2. CONSTRUCCIÓN DE LA APLICACIÓN Y CONFIGURACIÓN DEL PIPELINE
// ======================================================================

var app = builder.Build();

// --------------------------------------------
// 2.1 Swagger solo en modo desarrollo
// --------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// --------------------------------------------
// 2.2 Redireccionar HTTP → HTTPS
// --------------------------------------------
app.UseHttpsRedirection();


// --------------------------------------------
// 2.3 MIDDLEWARE DEL PIPELINE
// --------------------------------------------
// ⚠ ¡IMPORTANTE! Authentication SIEMPRE antes que Authorization
// Authentication = Identifica al usuario
// Authorization  = Verifica permisos del usuario

app.UseAuthentication();   // Lee el JWT

app.UseAuthorization();    // Revisa qué permisos tiene el usuario


// --------------------------------------------
// 2.4 Mapear controladores
// --------------------------------------------
app.MapControllers();


// --------------------------------------------
// 2.5 Ejecutar la aplicación
// --------------------------------------------
app.Run();
