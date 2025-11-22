using InventarioApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs
{
    public class ProductoDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        public decimal Precio { get; set; }

        [MaxLength(200, ErrorMessage = "La Descripcion es demasiado larga, resumela")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Stock es obligatorio")]
        public int StockActual { get; set; }

        public string UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }

        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }

        public int ProvedorId { get; set; }
        public string ProvedorNombre { get; set; }

    }
}
