using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioApi.Models;
using InventarioApi.Services;
using System.Security.Claims;
using InventarioApi.DTOs;


namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoService _ProductoService;

        public ProductoController(IProductoService productoService)
        {
            _ProductoService = productoService;
        }

        //obtener ID del usuario Logueado
        private string ObtenerUsuarioId()
        {
            // El ID del usuario viene dentro del token JWT como Claim
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var UsuarioId = ObtenerUsuarioId();
            var Productos = await _ProductoService.GetAllProductos(UsuarioId);

            return Ok(Productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            var usuarioId = ObtenerUsuarioId();
            var Producto = await _ProductoService.GetProductoById(id, usuarioId);

            if (Producto == null)
                NotFound("Producto No Encontrado");

            return Ok(Producto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto(ProductoCreateDTO NuevoProducto)
        {
            //se verifican las validaciones
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = ObtenerUsuarioId();

            var ProductoNuevo = await _ProductoService.CreateProducto(NuevoProducto, usuarioId);

            if (ProductoNuevo == null)
                return StatusCode(500, "Error al crear producto");

            return CreatedAtAction(nameof(GetProductoById), new { id = ProductoNuevo.Id }, ProductoNuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id ,ProductoUpdateDTO ProductoUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = ObtenerUsuarioId();

            var ProductoActualizado = await _ProductoService.UpdateProducto(id, ProductoUpdate, usuarioId);

            if (ProductoActualizado == null)
                return NotFound("Producto No Encontrado");

            return Ok(ProductoActualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var usuarioId = ObtenerUsuarioId();

            var ProductoEliminado = await _ProductoService.DeleteProducto(id, usuarioId);

            if(!ProductoEliminado)
                return NotFound("Producto No Encontrado");

            return NoContent();
        }
    }
}
