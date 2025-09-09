using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Mottu.Infrastructure.Data;

namespace Mottu.API.Services
{
    public class UsuarioPatioService : IUsuarioPatioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioPatioService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioPatioDTO>> GetAllAsync()
        {
            var usuarios = await _context.UsuariosPatio.Include(u => u.Patio).ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioPatioDTO>>(usuarios);
        }

        public async Task<UsuarioPatioDTO> GetByIdAsync(int id)
        {
            var usuario = await _context.UsuariosPatio.FindAsync(id);
            if (usuario is null) throw new Exception("Usuário não encontrado");
            return _mapper.Map<UsuarioPatioDTO>(usuario);
        }

        public async Task<UsuarioPatioDTO> CreateAsync(UsuarioPatioDTO dto)
        {
            var patio = await _context.Patios.FindAsync(dto.PatioId);
            if (patio is null) throw new Exception("Pátio não encontrado");

            var usuario = new UsuarioPatio
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Funcao = dto.Funcao,
                PatioId = patio.Id,
                Patio = patio
            };

            _context.UsuariosPatio.Add(usuario);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioPatioDTO>(usuario);
        }

        public async Task<UsuarioPatioDTO> UpdateAsync(int id, UsuarioPatioDTO dto)
        {
            var usuario = await _context.UsuariosPatio.FindAsync(id);
            if (usuario is null) throw new Exception("Usuário não encontrado");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Funcao = dto.Funcao;
            usuario.PatioId = dto.PatioId;

            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioPatioDTO>(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.UsuariosPatio.FindAsync(id);
            if (usuario is null) throw new Exception("Usuário não encontrado");

            _context.UsuariosPatio.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
