using MotoVision.Application.DTOs;

namespace MotoVision.Application.Interfaces
{
    public interface IPatioRepository
    {
        Task<IEnumerable<PatioDto>> GetAllAsync();
        Task<PatioDto> GetByIdAsync(int id);
        Task<PatioDto> CreateAsync(PatioDto dto);
        Task<PatioDto> UpdateAsync(int id, PatioDto dto);
        Task DeleteAsync(int id);
        Task<object> GetStatusAsync(int id);
    }
}

