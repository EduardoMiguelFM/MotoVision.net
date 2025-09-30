using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;
using Mottu.Domain.Entities;
using Mottu.Domain.Enums;
using Mottu.Domain.ValueObjects;
using Mottu.Infrastructure.Data;

namespace Mottu.API.Services
{
    public class MotoService : IMotoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MotoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MotoResponseDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var motos = await _context.Motos
                .Include(m => m.Patio)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MotoResponseDto>>(motos);
        }

        public async Task<MotoResponseDto> GetByIdAsync(int id)
        {
            var moto = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (moto is null) throw new Exception("Moto não encontrada");
            return _mapper.Map<MotoResponseDto>(moto);
        }

        public async Task<MotoResponseDto> GetByPlacaAsync(string placa)
        {
            var moto = await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Placa.Valor == placa.ToUpper());

            if (moto is null) throw new Exception("Moto não encontrada");
            return _mapper.Map<MotoResponseDto>(moto);
        }

        public async Task<IEnumerable<MotoResponseDto>> GetByStatusAsync(StatusMoto status)
        {
            var motos = await _context.Motos
                .Include(m => m.Patio)
                .Where(m => m.Status == status)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MotoResponseDto>>(motos);
        }

        public async Task<IEnumerable<MotoResponseDto>> GetFilteredAsync(StatusMoto? status = null, string? setor = null, string? cor = null)
        {
            var query = _context.Motos.Include(m => m.Patio).AsQueryable();

            if (status.HasValue)
                query = query.Where(m => m.Status == status.Value);

            if (!string.IsNullOrEmpty(setor))
                query = query.Where(m => m.SetorCor.Setor == setor);

            if (!string.IsNullOrEmpty(cor))
                query = query.Where(m => m.SetorCor.Cor == cor);

            var motos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<MotoResponseDto>>(motos);
        }

        public async Task<MotoResponseDto> CreateAsync(MotoDto dto)
        {
            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.Id == dto.PatioId);
            if (patio is null) throw new Exception("Pátio não encontrado");

            var moto = new Moto(dto.Modelo, new Placa(dto.Placa), patio);
            moto.DefinirStatus(dto.Status);

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();

            return _mapper.Map<MotoResponseDto>(moto);
        }

        public async Task<MotoResponseDto> UpdateAsync(int id, MotoDto dto)
        {
            var moto = await _context.Motos.Include(m => m.Patio).FirstOrDefaultAsync(m => m.Id == id);
            if (moto is null) throw new Exception("Moto não encontrada");

            var patio = await _context.Patios.FirstOrDefaultAsync(p => p.Id == dto.PatioId);
            if (patio is null) throw new Exception("Pátio não encontrado");

            moto.MoverPara(patio);
            moto.DefinirStatus(dto.Status);
            await _context.SaveChangesAsync();

            return _mapper.Map<MotoResponseDto>(moto);
        }

        public async Task DeleteAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto is null) throw new Exception("Moto não encontrada");

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountBySetorAsync(string setor)
        {
            return await _context.Motos
                .CountAsync(m => m.SetorCor.Setor == setor);
        }

        public async Task<StatusMoto> GetStatusByPlacaAsync(string placa)
        {
            var moto = await _context.Motos
                .FirstOrDefaultAsync(m => m.Placa.Valor == placa.ToUpper());

            if (moto is null) throw new Exception("Moto não encontrada");
            return moto.Status;
        }
    }
}