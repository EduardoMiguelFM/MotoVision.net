using Mottu.Application.DTOs;

namespace Mottu.Application.Interfaces
{
    public interface IMotoRepository
    {
        Task<IEnumerable<MotoDTO>> GetAllAsync();
        Task<MotoDTO> GetByIdAsync(int id);
        Task<MotoDTO> CreateAsync(MotoDTO dto);
        Task<MotoDTO> UpdateAsync(int id, MotoDTO dto);
        Task DeleteAsync(int id);
    }
}