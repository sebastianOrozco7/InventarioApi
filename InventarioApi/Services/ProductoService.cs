using AutoMapper;
using InventarioApi.Data;
using InventarioApi.DTOs.ProductoDto;
using InventarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public ProductoService(AppDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductoDTO>> GetAllProductos(string usuarioId)
        {
            var Productos = await _Context.Productos
                .Include(p => p.Usuario)
                .Include(p => p.Categoria)
                .Include(p => p.Provedor)
                .Where(p => p.UsuarioId ==  usuarioId)
                .ToListAsync();

            return _mapper.Map<List<ProductoDTO>>(Productos);
        }
        public async Task<ProductoDTO> GetProductoById(int id, string usuarioId)
        {
            var Producto = await _Context.Productos
                .Include(p => p.Usuario)
                .Include(p => p.Categoria)
                .Include(p => p.Provedor)
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.Id == id);

            return _mapper.Map<ProductoDTO>(Producto);
        }

        public async Task<ProductoDTO> CreateProducto(ProductoCreateDTO productoNuevoDTO, string usuarioId)
        { 
            //mapeo del dto del usuario a la entidad principal Producto  DTO --> Producto
            var ProductoNuevo = _mapper.Map<Producto>(productoNuevoDTO);

            //asignamos el usuario 
            ProductoNuevo.UsuarioId = usuarioId;

            //creamos el objeto en la base de datos
            _Context.Productos.Add(ProductoNuevo);
            await _Context.SaveChangesAsync();

            //devuelvo el producto con sus relaciones
            return await GetProductoById(ProductoNuevo.Id, usuarioId);
        }

        public async Task<ProductoDTO> UpdateProducto(int id, ProductoUpdateDTO productoUpdateDTO, string usuarioId)
        {
            //verifico si el producto existe
            var producto = await _Context.Productos
                .Where(p => p.Id == id && p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (producto == null)
                return null;

            //mapeo de los cambios realizados al producto
            _mapper.Map(productoUpdateDTO, producto);
            await _Context.SaveChangesAsync();

            //devuelvo el producto con sus relaciones
            return await GetProductoById(producto.Id, usuarioId);
        }

        public async Task<bool> DeleteProducto(int id, string usuarioId)
        {
            var producto = await _Context.Productos
                .Where(p => p.Id == id && p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (producto == null)
                return false;

            _Context.Productos.Remove(producto);
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
