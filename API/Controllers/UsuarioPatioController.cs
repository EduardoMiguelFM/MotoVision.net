﻿namespace Mottu.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Mottu.Application.DTOs;
    using Mottu.Application.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPatioController : ControllerBase
    {
        private readonly IUsuarioPatioService _service;

        public UsuarioPatioController(IUsuarioPatioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioPatioDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioPatioDTO dto) => Ok(await _service.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
