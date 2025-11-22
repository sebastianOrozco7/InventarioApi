using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio {  get; set; }
        public string Descripcion {  get; set; }
        public int StockActual {  get; set; }

        public string UsuarioId {  get; set; }
        public IdentityUser Usuario {  get; set; }

        public int CategoriaId {  get; set; }
        public Categoria Categoria { get; set; }

        public int ProvedorId {  get; set; }
        public Provedor Provedor {  get; set; }

        public List<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();

    }
}
