using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MotoVision.Application.DTOs;
using MotoVision.Application.Interfaces;
using MotoVision.Domain.Entities;
using MotoVision.Infrastructure.Data;

namespace MotoVision.API.Services
{
    public class UsuarioService : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null) throw new Exception("Usuário não encontrado");
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                CPF = dto.CPF,
                Funcao = dto.Funcao
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> UpdateAsync(int id, UsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario is null) throw new Exception("Usuário não encontrado");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;
            usuario.CPF = dto.CPF;
            usuario.Funcao = dto.Funcao;

            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is null) throw new Exception("Usuário não encontrado");
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}

