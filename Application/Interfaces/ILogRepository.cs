using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<MotoLog>> GetAllAsync();
        Task AddAsync(MotoLog log);
    }
}
