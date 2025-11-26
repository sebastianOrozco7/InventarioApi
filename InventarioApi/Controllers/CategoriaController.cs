using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventarioApi.Models;
using InventarioApi.Services;
using System.Security.Claims;
using InventarioApi.DTOs.ProductoDto;
using InventarioApi.DTOs.CategoriaDto;


namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController  : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        private string ObtenerUsuarioId()
        {
            // El ID del usuario viene dentro del token JWT como Claim
            return User.FindFirstValue("userId")!; // utilice el Claim personalizado para evitar algun conflicto y ser mas preciso
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var Usuario = ObtenerUsuarioId();
            var Categorias = await _categoriaService.GetAllCategorias(Usuario);

            return Ok(Categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var Usuario = ObtenerUsuarioId();
            var categoria = await _categoriaService.GetCategoriaById(id, Usuario);

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoria(CategoriaCreateUpdateDTO Categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Usuario = ObtenerUsuarioId();

            var CategoriaNueva = await _categoriaService.CreateCategoria(Categoria, Usuario);

            if (CategoriaNueva == null)
                return StatusCode(500, "Error al crear producto");

            //return CreatedAtAction(nameof(GetProductoById), new { id = ProductoNuevo.Id }, ProductoNuevo);
            return Ok(CategoriaNueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, CategoriaCreateUpdateDTO categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Usuario = ObtenerUsuarioId();

            var CategoriaUpdate = await _categoriaService.UpdateCategoria(id, categoria, Usuario);

            if (CategoriaUpdate == null)
                return StatusCode(500, "error al Actualizar Categoria");

            return Ok(CategoriaUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var Usuario = ObtenerUsuarioId();

            var CategoriaDelete = await _categoriaService.DeleteCategoria(id, Usuario);

            if(!CategoriaDelete)
                return NotFound("Producto No Encontrado");

            return NoContent();
        }

    }
}

