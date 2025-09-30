using Mottu.Application.DTOs;
using Mottu.Domain.Enums;

namespace Mottu.Application.Interfaces
{
    public interface IMotoRepository
    {
        Task<IEnumerable<MotoResponseDto>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<MotoResponseDto> GetByIdAsync(int id);
        Task<MotoResponseDto> GetByPlacaAsync(string placa);
        Task<IEnumerable<MotoResponseDto>> GetByStatusAsync(StatusMoto status);
        Task<IEnumerable<MotoResponseDto>> GetFilteredAsync(StatusMoto? status = null, string? setor = null, string? cor = null);
        Task<MotoResponseDto> CreateAsync(MotoDto dto);
        Task<MotoResponseDto> UpdateAsync(int id, MotoDto dto);
        Task DeleteAsync(int id);
        Task<int> GetCountBySetorAsync(string setor);
        Task<StatusMoto> GetStatusByPlacaAsync(string placa);
    }
}