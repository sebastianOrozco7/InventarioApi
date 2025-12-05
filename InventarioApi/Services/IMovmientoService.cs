using InventarioApi.DTOs.MovimientoDto;

namespace InventarioApi.Services
{
    public interface IMovmientoService
    {
        Task<List<MovimientoInventarioDTO>> GetAll(string usuarioId);
        Task<MovimientoInventarioDTO> GetById(int Id, string usuarioId);
        Task<bool> CrearMovimiento(MovimientoDTO movimiento, string usuarioId);
    }
}
