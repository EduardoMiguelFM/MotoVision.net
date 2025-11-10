using MotoVision.Domain.Entities;

namespace MotoVision.Domain.Repositories
{
    public interface IPatioRepository
    {
        Task CreateAsync(Patio patio);
        Task<Patio?> GetByIdAsync(int id);
        Task<IEnumerable<Patio>> GetAllAsync();
        Task UpdateAsync(Patio patio);
        Task DeleteAsync(int id);
    }
}
