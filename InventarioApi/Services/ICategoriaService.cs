using InventarioApi.DTOs.CategoriaDto;

namespace InventarioApi.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> GetAllCategorias(string UsuarioId);
        Task<CategoriaDTO> GetCategoriaById(int id, string usuario);
        Task<CategoriaDTO> CreateCategoria(CategoriaCreateUpdateDTO CategoriaNueva, string usuarioId);
        Task<CategoriaDTO> UpdateCategoria(int id, CategoriaCreateUpdateDTO CategoriaUpdate, string usuarioId);
        Task<bool> DeleteCategoria(int id, string usuarioId);
    }
}
