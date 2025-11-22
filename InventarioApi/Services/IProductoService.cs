using InventarioApi.DTOs;

namespace InventarioApi.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> GetAllProductos(string UsuarioId);
        Task<ProductoDTO> GetProductoById(int id, string usuario);

        Task<ProductoDTO> CreateProducto(ProductoCreateDTO productoNuevo, string usuarioId);
        Task<ProductoDTO> UpdateProducto(int id, ProductoUpdateDTO productoUpdate, string usuarioId);
        Task<bool> DeleteProducto(int id, string usuarioId);
    }
}
