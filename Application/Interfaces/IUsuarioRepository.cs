using MotoVision.Application.DTOs;

namespace MotoVision.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task<UsuarioDto> CreateAsync(UsuarioDto dto);
        Task<UsuarioDto> UpdateAsync(int id, UsuarioDto dto);
        Task DeleteAsync(int id);
    }
}

