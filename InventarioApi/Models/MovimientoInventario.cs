using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public int ProductoId {  get; set; }
        public Producto producto { get; set; }

        public DateTime Fecha {  get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int Cantidad {  get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        public string Tipo {  get; set; } // entrada o salida

        [MaxLength(200, ErrorMessage = "La Descripcion es demasiado larga, resumela")]
        public string Observacion {  get; set; }
    }
}
