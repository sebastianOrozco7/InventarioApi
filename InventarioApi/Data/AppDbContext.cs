using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InventarioApi.Models;
using Microsoft.AspNetCore.Identity;

namespace InventarioApi.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { 

        }

        public DbSet<Categoria> Categorias {  get; set; }
        public DbSet<Provedor> provedores { get; set; }
        public DbSet<Producto> Productos {  get; set; }
        public DbSet<MovimientoInventario> Movimientos {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // relacion categoria ---> productos
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.categoria)
                .HasForeignKey(p => p.CategoriaId);

            //relacion provedor ---> prodcutos 
            modelBuilder.Entity<Provedor>()
                .HasMany(pr => pr.productos)
                .WithOne(p => p.provedor)
                .HasForeignKey(p => p.ProvedorId);

            //relacion producto ---> movimientos
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Movimientos)
                .WithOne(m => m.producto)
                .HasForeignKey(m => m.ProductoId);
        }
    }

}
