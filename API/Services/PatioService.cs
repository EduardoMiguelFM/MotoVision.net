using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Mottu.Infrastructure.Data;

namespace Mottu.API.Services
{
    public class PatioService : IPatioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatioService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatioDto>> GetAllAsync()
        {
            var patios = await _context.Patios.ToListAsync();
            return _mapper.Map<IEnumerable<PatioDto>>(patios);
        }

        public async Task<PatioDto> GetByIdAsync(int id)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.Id == id);
            if (patio is null) throw new Exception("Pátio não encontrado");
            return _mapper.Map<PatioDto>(patio);
        }

        public async Task<PatioDto> CreateAsync(PatioDto dto)
        {
            var patio = new Patio(dto.Nome, dto.Endereco);
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
            return _mapper.Map<PatioDto>(patio);
        }

        public async Task<PatioDto> UpdateAsync(int id, PatioDto dto)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.Id == id);
            if (patio is null) throw new Exception("Pátio não encontrado");

            // Atualizar propriedades usando reflection ou métodos específicos
            var updatedPatio = new Patio(dto.Nome, dto.Endereco);
            _context.Entry(patio).CurrentValues.SetValues(updatedPatio);
            await _context.SaveChangesAsync();
            return _mapper.Map<PatioDto>(patio);
        }

        public async Task DeleteAsync(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio is null) throw new Exception("Pátio não encontrado");
            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetStatusAsync(int id)
        {
            var patio = await _context.Patios
                .Include(p => p.Motos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patio is null) throw new Exception("Pátio não encontrado");

            var status = new
            {
                PatioId = patio.Id,
                NomePatio = patio.Nome,
                TotalMotos = patio.Motos.Count,
                MotosDisponiveis = patio.Motos.Count(m => m.Status == Domain.Enums.StatusMoto.DISPONIVEL),
                MotosReservadas = patio.Motos.Count(m => m.Status == Domain.Enums.StatusMoto.RESERVADA),
                MotosManutencao = patio.Motos.Count(m => m.Status == Domain.Enums.StatusMoto.MANUTENCAO),
                MotosIndisponiveis = patio.Motos.Count(m => m.Status == Domain.Enums.StatusMoto.INDISPONIVEL)
            };

            return status;
        }
    }
}