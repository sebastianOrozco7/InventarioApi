using Microsoft.AspNetCore.Identity;

namespace InventarioApi.DTOs.ProvedorDto
{
    public class ProvedorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
    }
}
