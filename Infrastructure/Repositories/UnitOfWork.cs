using Mottu.Application.Interfaces;
using Mottu.Infrastructure.Data;

namespace Mottu.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        public UnitOfWork(ApplicationDbContext ctx) => _ctx = ctx;
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _ctx.SaveChangesAsync(ct);
    }
}