using InventarioApi.Services;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Categoria
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre {  get; set; }

        [MaxLength(200, ErrorMessage = "La Descripcion es demasiado larga, resumela")]
        public string Descripcion {  get; set; }

        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
