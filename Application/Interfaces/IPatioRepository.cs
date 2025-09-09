using Mottu.Application.DTOs;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface IPatioRepository
    {
        Task<IEnumerable<PatioDTO>> GetAllAsync();
        Task<PatioDTO> GetByIdAsync(int id);
        Task<PatioDTO> CreateAsync(PatioDTO dto);
        Task<Patio?> GetByNameAsync(string nome);
        Task<PatioDTO> UpdateAsync(int id, PatioDTO dto);
        Task DeleteAsync(int id);

        Task<object> ObterContagemPorSetorAsync(string setor);
        Task<object> ObterStatusPorPlacaAsync(string placa);
        Task<Dictionary<string, int>> ObterResumoStatusAsync();
    }
}