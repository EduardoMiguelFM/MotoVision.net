using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;

namespace Mottu.API.Controllers
{
    // Aplica a versão 1.0
    [ApiVersion("1.0")]
    [ApiController]
    // Rota com o marcador de versão e nome do recurso (no plural e hifenizado)
    [Route("api/v{version:apiVersion}/usuario-patios")]
    public class UsuarioPatioController : ControllerBase
    {
        private readonly IUsuarioPatioRepository _service;

        public UsuarioPatioController(IUsuarioPatioRepository service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as associações de usuário-pátio
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Busca associação de usuário-pátio por ID
        /// </summary>
        /// <param name="id">ID da associação</param>
        /// <returns>Dados da associação</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        /// <summary>
        /// Cria uma nova associação de usuário-pátio
        /// </summary>
        /// <param name="dto">Dados da associação</param>
        /// <returns>Associação criada</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioPatioDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            // Inclui o parâmetro 'version = "1.0"' para garantir o Location Header correto
            return CreatedAtAction(nameof(Get), new { version = "1.0", id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza uma associação de usuário-pátio existente
        /// </summary>
        /// <param name="id">ID da associação</param>
        /// <param name="dto">Novos dados da associação</param>
        /// <returns>Associação atualizada</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioPatioDTO dto)
            => Ok(await _service.UpdateAsync(id, dto));

        /// <summary>
        /// Remove uma associação de usuário-pátio
        /// </summary>
        /// <param name="id">ID da associação</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}