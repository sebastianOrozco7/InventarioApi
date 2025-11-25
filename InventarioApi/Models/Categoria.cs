using InventarioApi.Services;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Categoria
    {
        public int Id {  get; set; }
        public string Nombre {  get; set; }
        public string Descripcion {  get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
