using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventarioApi.Services;
using System.Security.Claims;
using InventarioApi.DTOs.MovimientoDto;

namespace InventarioApi.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    [Authorize]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovmientoService _movmientoService;

        public MovimientoController(IMovmientoService movmientoService)
        {
            _movmientoService = movmientoService;
        }

        private string ObtenerUsuario()
        {
            return User.FindFirstValue("userId")!;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuario = ObtenerUsuario();
            var Movimientos = await _movmientoService.GetAll(usuario);

            return Ok(Movimientos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = ObtenerUsuario();
            var Movimiento = await _movmientoService.GetById(id, usuario);

            return Ok(Movimiento);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovimiento([FromBody] MovimientoDTO MovimientoDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = ObtenerUsuario();

            var Movimiento = await _movmientoService.CrearMovimiento(MovimientoDto, usuario);

            if (!Movimiento)
                return StatusCode(500, "Error al crear movimiento de inventario");

            return Ok("Movimiento registrado correctamente correctamente");
        }
    }
}
