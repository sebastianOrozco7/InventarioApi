using InventarioApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        // -------------------------
        //  REGISTRO
        // -------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioDto usurio)
        {
            var user = new IdentityUser
            {
                UserName = usurio.Email,
                Email = usurio.Email
            };

            var result = await _userManager.CreateAsync(user, usurio.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Usuario registrado correctamente");
        }

        // -------------------------
        //  LOGIN
        // -------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioDto usuario)
        {
            var user = await _userManager.FindByEmailAsync(usuario.Email);

            if (user == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var passwordCorrecta = await _userManager.CheckPasswordAsync(user, usuario.Password);

            if (!passwordCorrecta)
                return Unauthorized("Usuario o contraseña incorrectos");

            // GENERAR TOKEN
            var token = GenerarToken(user);

            return Ok(new { token });
        }

        // -------------------------
        //  GENERAR JWT
        // -------------------------
        private string GenerarToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("userId", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
