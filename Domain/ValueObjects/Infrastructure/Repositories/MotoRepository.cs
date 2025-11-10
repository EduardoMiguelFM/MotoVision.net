using Microsoft.EntityFrameworkCore;
using MotoVision.Domain.Entities;
using MotoVision.Domain.Repositories;
using MotoVision.Infrastructure.Data;

namespace MotoVision.Infrastructure.Repositories

{
    /// <summary>
    /// Implementação do repositório de motos.
    /// Responsável pelo acesso direto ao banco via Entity Framework.
    /// </summary>
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationDbContext _context;

        public MotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Moto?> ObterPorIdAsync(int id)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Moto>> ListarAsync()
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Moto moto)
        {
            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Moto moto)
        {
            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
