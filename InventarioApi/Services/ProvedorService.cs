using AutoMapper;
using InventarioApi.Data;
using InventarioApi.Models;
using InventarioApi.DTOs.ProvedorDto;
using Microsoft.EntityFrameworkCore;
using InventarioApi.DTOs.Provedor;

namespace InventarioApi.Services
{
    public class ProvedorService : IProvedorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProvedorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProvedorDTO>> GetAllProvedor(string usuarioId)
        {
            var Provedores = await _context.provedores
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();

            return _mapper.Map<List<ProvedorDTO>>(Provedores);
        }
       public async Task<ProvedorDTO> GetProvedorById(int id, string usuario)
        {
            var Provedor = await _context.provedores
              .Include(p => p.Usuario)
              .FirstOrDefaultAsync(p => p.UsuarioId == usuario && p.Id == id);

            return _mapper.Map<ProvedorDTO>(Provedor);
        }

        public async Task<ProvedorDTO> CreateProvedor(ProvedorCreateUpdateDTO ProvedorNuevoDTO, string usuarioId)
        {
            var Provedor = _mapper.Map<Provedor>(ProvedorNuevoDTO);

            Provedor.UsuarioId = usuarioId;

            _context.provedores.Add(Provedor);
            await _context.SaveChangesAsync();

            return await GetProvedorById(Provedor.Id, usuarioId);
        }
        public async Task<ProvedorDTO> UpdateProvedor(int id, ProvedorCreateUpdateDTO ProvedorUpdateDTO, string usuarioId)
        {
            var provedor = await _context.provedores
                .Where(p => p.Id == id && p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (provedor == null)
                return null;

            _mapper.Map(ProvedorUpdateDTO, provedor);
            await _context.SaveChangesAsync();

            return await GetProvedorById(provedor.Id, usuarioId);
        }
        public async Task<bool> DeleteProvedor(int id, string usuarioId)
        {
            var provedor = await _context.provedores
              .Where(p => p.Id == id && p.UsuarioId == usuarioId)
              .FirstOrDefaultAsync();

            if (provedor == null)
                return false;

            _context.provedores.Remove(provedor);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
