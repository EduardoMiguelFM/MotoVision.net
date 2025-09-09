using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;

namespace Mottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoRepository _service;

        public MotoController(IMotoRepository service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotoDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] MotoDTO dto)
            => Ok(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // Endpoint extra para alterar status via rota (opcional)
        [HttpPut("{id:int}/status/{novoStatus}")]
        public async Task<IActionResult> AlterarStatus(int id, string novoStatus)
        {
            var atual = await _service.GetByIdAsync(id);
            var dto = new MotoDTO
            {
                Id = id,
                Modelo = atual.Modelo,
                Placa = atual.Placa,
                Status = novoStatus,
                NomePatio = atual.NomePatio
            };
            return Ok(await _service.UpdateAsync(id, dto));
        }
    }
}