using InventarioApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.DTOs.MovimientoDto
{
    public class MovimientoInventarioDTO
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string ProductoNombre { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } // entrada o salida
        public string Observacion { get; set; }
     
    }
}
