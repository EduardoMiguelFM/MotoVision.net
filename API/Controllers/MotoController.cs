using Microsoft.AspNetCore.Mvc;
using Mottu.Application.DTOs;
using Mottu.Application.Interfaces;
using Mottu.Domain.Enums;

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

        /// <summary>
        /// Lista todas as motos com paginação
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <returns>Lista paginada de motos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
            => Ok(await _service.GetAllAsync(page, pageSize));

        /// <summary>
        /// Busca moto por ID
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Dados da moto</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        /// <summary>
        /// Busca moto por placa
        /// </summary>
        /// <param name="placa">Placa da moto</param>
        /// <returns>Dados da moto</returns>
        [HttpGet("placa/{placa}")]
        public async Task<IActionResult> GetByPlaca(string placa)
            => Ok(await _service.GetByPlacaAsync(placa));

        /// <summary>
        /// Filtra motos por status
        /// </summary>
        /// <param name="status">Status da moto</param>
        /// <returns>Lista de motos com o status especificado</returns>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(StatusMoto status)
            => Ok(await _service.GetByStatusAsync(status));

        /// <summary>
        /// Filtro avançado de motos
        /// </summary>
        /// <param name="status">Status da moto (opcional)</param>
        /// <param name="setor">Setor da moto (opcional)</param>
        /// <param name="cor">Cor do setor (opcional)</param>
        /// <returns>Lista de motos filtradas</returns>
        [HttpGet("filtro")]
        public async Task<IActionResult> GetFiltered([FromQuery] StatusMoto? status = null, [FromQuery] string? setor = null, [FromQuery] string? cor = null)
            => Ok(await _service.GetFilteredAsync(status, setor, cor));

        /// <summary>
        /// Cria uma nova moto
        /// </summary>
        /// <param name="dto">Dados da moto</param>
        /// <returns>Moto criada</returns>
        /// <response code="201">Moto criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Pátio não encontrado</response>
        [HttpPost]
        [ProducesResponseType(typeof(MotoResponseDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Post([FromBody] MotoDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza uma moto existente
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <param name="dto">Novos dados da moto</param>
        /// <returns>Moto atualizada</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] MotoDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        /// <summary>
        /// Remove uma moto
        /// </summary>
        /// <param name="id">ID da moto</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Conta motos por setor
        /// </summary>
        /// <param name="setor">Nome do setor</param>
        /// <returns>Quantidade de motos no setor</returns>
        [HttpGet("patio/setor/{setor}/contagem")]
        public async Task<IActionResult> GetCountBySetor(string setor)
        {
            var count = await _service.GetCountBySetorAsync(setor);
            return Ok(new { Setor = setor, Quantidade = count });
        }

        /// <summary>
        /// Obtém status de uma moto por placa
        /// </summary>
        /// <param name="placa">Placa da moto</param>
        /// <returns>Status da moto</returns>
        [HttpGet("patio/moto/{placa}/status")]
        public async Task<IActionResult> GetStatusByPlaca(string placa)
        {
            var status = await _service.GetStatusByPlacaAsync(placa);
            return Ok(new { Placa = placa, Status = status.ToString() });
        }
    }
}