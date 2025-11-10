using System.Collections.Generic;
using System.Threading.Tasks;
using MotoVision.Domain.Entities;

namespace MotoVision.Domain.Repositories
{
    /// <summary>
    /// Interface do repositório de motos.
    /// Define as operações de acesso e persistência da entidade Moto.
    /// </summary>
    public interface IMotoRepository
    {
        Task<Moto?> ObterPorIdAsync(int id);
        Task<IEnumerable<Moto>> ListarAsync();
        Task AdicionarAsync(Moto moto);
        Task AtualizarAsync(Moto moto);
        Task RemoverAsync(int id);
    }
}
