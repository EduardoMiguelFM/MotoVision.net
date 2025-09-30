using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;

namespace Mottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatiosController : ControllerBase
    {
        private readonly IPatioRepository _service;

        public PatiosController(IPatioRepository service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os pátios
        /// </summary>
        /// <returns>Lista de pátios</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Busca pátio por ID
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Dados do pátio</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        /// <summary>
        /// Obtém status geral do pátio
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Status do pátio com contagem de motos</returns>
        [HttpGet("{id:int}/status")]
        public async Task<IActionResult> GetStatus(int id)
            => Ok(await _service.GetStatusAsync(id));

        /// <summary>
        /// Cria um novo pátio
        /// </summary>
        /// <param name="dto">Dados do pátio</param>
        /// <returns>Pátio criado</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatioDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza um pátio existente
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <param name="dto">Novos dados do pátio</param>
        /// <returns>Pátio atualizado</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PatioDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        /// <summary>
        /// Remove um pátio
        /// </summary>
        /// <param name="id">ID do pátio</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
