using MotoVision.Domain.Entities;

namespace MotoVision.Application.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<MotoLog>> GetAllAsync();
        Task AddAsync(MotoLog log);
    }
}

