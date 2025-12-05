using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs.MovimientoDto
{
    public class MovimientoDTO
    {
        [Required(ErrorMessage = "El id es obligatorio")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        public string Tipo { get; set; } // entrada o salida

        [MaxLength(200, ErrorMessage = "La Descripcion es demasiado larga, resumela")]
        public string Observacion { get; set; }
    }
}
