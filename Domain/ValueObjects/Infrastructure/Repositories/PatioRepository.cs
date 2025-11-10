using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotoVision.Domain.Entities;
using MotoVision.Domain.Repositories;
using MotoVision.Application.Interfaces;

namespace MotoVision.Infrastructure.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly ApplicationDbContext _context;

        public PatioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patio?> GetByIdAsync(int id)
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patio>> GetAllAsync()
        {
            return await _context.Patios
                .Include(p => p.Motos)
                .ToListAsync();
        }

        public async Task CreateAsync(Patio patio)
        {
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patio patio)
        {
            _context.Patios.Update(patio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio != null)
            {
                _context.Patios.Remove(patio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
