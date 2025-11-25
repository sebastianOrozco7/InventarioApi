using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Provedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email {  get; set; }
        public string UsuarioId {  get; set; }
        public IdentityUser Usuario { get; set; }
        public List<Producto> productos { get; set; } = new List<Producto>();
    }
}
