using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;

namespace Mottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : ControllerBase
    {
        private readonly IPatioRepository _patioService;

        public PatioController(IPatioRepository patioService)
        {
            _patioService = patioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _patioService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _patioService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatioDto dto)
        {
            var result = await _patioService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PatioDto dto)
            => Ok(await _patioService.UpdateAsync(id, dto));

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