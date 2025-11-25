using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs.CategoriaDto
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string usuarioNombre { get; set; }
    }
}
