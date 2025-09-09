using Mottu.Application.DTOs;

namespace Mottu.Application.Interfaces
{
    public interface IUsuarioPatioRepository
    {
        Task<IEnumerable<UsuarioPatioDTO>> GetAllAsync();
        Task<UsuarioPatioDTO> GetByIdAsync(int id);
        Task<UsuarioPatioDTO> CreateAsync(UsuarioPatioDTO dto);
        Task<UsuarioPatioDTO> UpdateAsync(int id, UsuarioPatioDTO dto);
        Task DeleteAsync(int id);
    }
}