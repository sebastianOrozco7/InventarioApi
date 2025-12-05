using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventarioApi.Services;
using System.Security.Claims;
using InventarioApi.DTOs.ProvedorDto;
using InventarioApi.DTOs.Provedor;
using InventarioApi.DTOs.CategoriaDto;


namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProvedorController : ControllerBase
    {
        private readonly IProvedorService _ProvedorService;

        public ProvedorController(IProvedorService provedorService)
        {
            _ProvedorService = provedorService;
        }

        private string ObtenerUsuario()
        {
            return User.FindFirstValue("UserId")!;
        }

        [HttpGet]
        public async Task<IActionResult> GetProvedores()
        {
            var usuario = ObtenerUsuario();
            var provedores = await _ProvedorService.GetAllProvedor(usuario);

            return Ok(provedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdProvedor(int id)
        {
            var usuario = ObtenerUsuario();
            var provedor = await _ProvedorService.GetProvedorById(id, usuario);

            return Ok(provedor);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProvedor(ProvedorCreateUpdateDTO Provedor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = ObtenerUsuario();

            var ProvedorNuevo = await _ProvedorService.CreateProvedor(Provedor, usuario);


            if (ProvedorNuevo == null)
                return StatusCode(500, "Error al crear provedor");

            return Ok(ProvedorNuevo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvedor(int id, ProvedorCreateUpdateDTO Provedor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Usuario = ObtenerUsuario();

            var ProvedorUpdate = await _ProvedorService.UpdateProvedor(id, Provedor, Usuario);

            if (ProvedorUpdate == null)
                return StatusCode(500, "error al Actualizar Categoria");

            return Ok(ProvedorUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var Usuario = ObtenerUsuario();

            var Provedor = await _ProvedorService.DeleteProvedor(id,Usuario);

            if (!Provedor)
                return NotFound("Producto No Encontrado");

            return NoContent();
        }
    }
}
