using AutoMapper;
using InventarioApi.Data;
using InventarioApi.DTOs.MovimientoDto;
using InventarioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Services
{
    public class MovimientoService : IMovmientoService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _Context;

        public MovimientoService(AppDbContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        public async Task<List<MovimientoInventarioDTO>> GetAll(string usuarioId)
        {
            var Movimiento = await _Context.Movimientos
                .Include(m => m.Usuario)
                .Include(m => m.Producto)
                .Where(m => m.UsuarioId == usuarioId) 
                .ToListAsync();

            return _mapper.Map<List<MovimientoInventarioDTO>>(Movimiento);
        }
        public async Task<MovimientoInventarioDTO> GetById(int id, string usuarioId)
        {
            var Movimiento = await _Context.Movimientos
               .Include(m => m.Usuario)
               .Include(m => m.Producto)
               .FirstOrDefaultAsync(m => m.Id == id && m.UsuarioId == usuarioId);

            return _mapper.Map<MovimientoInventarioDTO>(Movimiento);
        }
        public async Task<bool> CrearMovimiento(MovimientoDTO movimientoDto, string usuarioId)
        {
            using var transaction = await _Context.Database.BeginTransactionAsync();

            try
            {
                var Producto = await _Context.Productos.FindAsync(movimientoDto.ProductoId);
                if (Producto == null)
                    throw new Exception("El Producto NO Existe");

                //mapeo del dto del usuario a la entidad principal MovimientoInventario  DTO --> Movimiento
                var Movimiento = _mapper.Map<MovimientoInventario>(movimientoDto);

                //asigno el usuario a el movimiento
                Movimiento.UsuarioId = usuarioId;

                _Context.Movimientos.Add(Movimiento);

                if (movimientoDto.Tipo == "ENTRADA")
                {
                    Producto.StockActual += movimientoDto.Cantidad;
                    _Context.Productos.Update(Producto);
                }
                   

                else if (movimientoDto.Tipo == "SALIDA")
                {
                    if (Producto.StockActual < movimientoDto.Cantidad)
                    {
                        throw new Exception("Stock del producto agotado");
                    }
                    else
                    {
                        Producto.StockActual -= movimientoDto.Cantidad;
                        _Context.Productos.Update(Producto);
                    }
                }

                await _Context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
               
            }
            catch(Exception Ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error en el movimiento: {Ex.Message}");
            }
        }
    }
}
