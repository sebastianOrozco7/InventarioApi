using AutoMapper;
using InventarioApi.Data;
using InventarioApi.DTOs.CategoriaDto;
using InventarioApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CategoriaDTO>> GetAllCategorias(string usuarioId)
        {
            var Categorias = await _context.Categorias
                .Include(c => c.Usuario)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            return _mapper.Map<List<CategoriaDTO>>(Categorias);
        }

        [HttpGet("{id}")]
        public async Task<CategoriaDTO> GetCategoriaById(int id, string usuario)
        {
            var Categoria = await _context.Categorias
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuario && c.Id == id);

            return _mapper.Map<CategoriaDTO>(Categoria);
        }

        [HttpPost]
        public async Task<CategoriaDTO> CreateCategoria(CategoriaCreateUpdateDTO CategoriaNuevaDTO, string usuarioId)
        {
            //mapeo del dto del usuario a la entidad principal Categoria  DTO --> Categoria
            var CategoriaNueva = _mapper.Map<Categoria>(CategoriaNuevaDTO);

            //Asigno el id del usuario
            CategoriaNueva.UsuarioId = usuarioId;

            _context.Categorias.Add(CategoriaNueva);
            await _context.SaveChangesAsync();

            return await GetCategoriaById(CategoriaNueva.Id, usuarioId);
        }

        [HttpPut("{id}")]
        public async Task<CategoriaDTO> UpdateCategoria(int id, CategoriaCreateUpdateDTO CategoriaUpdateDTO, string usuarioId)
        {
            var Categoria = await _context.Categorias
                .Where(c => c.UsuarioId == usuarioId && c.Id == id)
                .FirstOrDefaultAsync();

            if (Categoria == null)
                return null;

            _mapper.Map(CategoriaUpdateDTO, Categoria);
            await _context.SaveChangesAsync();

            return await GetCategoriaById(Categoria.Id, usuarioId);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCategoria(int id, string usuarioId)
        {

            var Categoria = await _context.Categorias
                .Where(c => c.UsuarioId == usuarioId && c.Id == id)
                .FirstOrDefaultAsync();

            if (Categoria == null)
                return false;

            _context.Categorias.Remove(Categoria);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
