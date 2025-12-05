using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs.CategoriaDto
{
    public class CategoriaCreateUpdateDTO
    {
        [Required (ErrorMessage = "El Nombre es Obligatorio")]
        public string Nombre { get; set; }

        [MaxLength (200, ErrorMessage = "Descripccion demasiado larga")]
        public string Descripcion { get; set; }
    }
}
