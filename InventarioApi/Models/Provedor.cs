using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Provedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El numero de Telefono es obligatorio")]
        [MaxLength(10, ErrorMessage = "El numero de Telefono no puede superar los 10 digitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        public string Email {  get; set; }

        public List<Producto> productos { get; set; } = new List<Producto>();
    }
}
