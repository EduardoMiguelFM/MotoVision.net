using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoVision.API.Services;
using MotoVision.Domain.Models;
using MotoVision.Application.DTOs; // <-- adicionado para os DTOs corretos
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotoVision.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de usuários.
    /// Inclui autenticação, cadastro e listagem.
    /// Versão 1.0.
    /// </summary>
    [Authorize] // Exige JWT por padrão
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public UsuariosController(UsuarioService usuarioService, TokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Autentica um usuário e gera um token JWT.
        /// </summary>
        /// <param name="login">Credenciais de login (email e senha)</param>
        /// <returns>Token JWT válido</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return BadRequest("Email e senha são obrigatórios.");

            var usuario = await _usuarioService.AutenticarAsync(login.Email, login.Senha);

            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            var token = _tokenService.GenerateToken(usuario);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UsuarioDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Registrar([FromBody] UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = await _usuarioService.RegistrarAsync(usuarioDto);
            if (novoUsuario == null)
                return BadRequest("Erro ao criar usuário.");

            return CreatedAtAction(nameof(ObterPorId), new { id = novoUsuario.Id, version = "1.0" }, novoUsuario);
        }

        /// <summary>
        /// Retorna um usuário pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Lista todos os usuários (requer autenticação).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), 200)]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuarioService.ListarAsync();
            return Ok(usuarios);
        }
    }
}

