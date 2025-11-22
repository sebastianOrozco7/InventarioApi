using InventarioApi.Models;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs
{
    public class ProductoUpdateDTO
    {

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        public decimal Precio { get; set; }

        [MaxLength(200, ErrorMessage = "La Descripcion es demasiado larga, resumela")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Stock es obligatorio")]
        public int StockActual { get; set; }

        [Required(ErrorMessage = "La Categoria es obligatorio")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El Provedor es obligatorio")]
        public int ProvedorId { get; set; }
    }
}
