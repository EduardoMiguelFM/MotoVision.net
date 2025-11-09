using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;

namespace Mottu.API.Controllers
{
    // Aplica a versão 1.0
    [ApiVersion("1.0")]
    [ApiController]
    // Rota explícita com o marcador de versão e o nome do recurso no plural
    [Route("api/v{version:apiVersion}/patios")]
    public class PatioController : ControllerBase
    {
        private readonly IPatioRepository _patioService;

        public PatioController(IPatioRepository patioService)
        {
            _patioService = patioService;
        }

        /// <summary>
        /// Lista todos os pátios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _patioService.GetAllAsync());

        /// <summary>
        /// Busca pátio por ID
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Dados do pátio</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _patioService.GetByIdAsync(id));

        /// <summary>
        /// Cria um novo pátio
        /// </summary>
        /// <param name="dto">Dados do pátio</param>
        /// <returns>Pátio criado</returns>
        /// <response code="201">Pátio criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatioDto dto)
        {
            var result = await _patioService.CreateAsync(dto);
            // Inclui o parâmetro 'version' para garantir que o redirecionamento (Location Header) 
            // aponte para a rota /api/v1/patios/{id}
            return CreatedAtAction(nameof(Get), new { version = "1.0", id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza um pátio existente
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <param name="dto">Novos dados do pátio</param>
        /// <returns>Pátio atualizado</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PatioDto dto)
            => Ok(await _patioService.UpdateAsync(id, dto));

        /// <summary>
        /// Remove um pátio
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _patioService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Obtém status geral do pátio
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Status do pátio com contagem de motos</returns>
        [HttpGet("{id:int}/status")]
        public async Task<IActionResult> GetStatus(int id)
            => Ok(await _patioService.GetStatusAsync(id));
    }
}