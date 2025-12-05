using InventarioApi.DTOs.CategoriaDto;
using InventarioApi.DTOs.Provedor;
using InventarioApi.DTOs.ProvedorDto;

namespace InventarioApi.Services
{
    public interface IProvedorService
    {
        Task<List<ProvedorDTO>> GetAllProvedor(string UsuarioId);
        Task<ProvedorDTO> GetProvedorById(int id, string usuario);
        Task<ProvedorDTO> CreateProvedor(ProvedorCreateUpdateDTO ProvedorNuevo, string usuarioId);
        Task<ProvedorDTO> UpdateProvedor(int id, ProvedorCreateUpdateDTO ProvedorUpdate, string usuarioId);
        Task<bool> DeleteProvedor(int id, string usuarioId);
    }
}
