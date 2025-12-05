using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs.Provedor
{
    public class ProvedorCreateUpdateDTO
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [MaxLength(12, ErrorMessage = "Numero de telefono muy largo")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        public string Email { get; set; }
      
    }
}
