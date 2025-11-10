using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoVision.API.Services;
using MotoVision.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace MotoVision.API.Controllers
{
    // Aplica a versão 1.0
    [ApiVersion("1.0")]
    [ApiController]
    // Rota com o marcador de versão: api/v1/auth
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // DTOs para requests
        public record RegisterRequest(string Nome, string Email, string Senha);
        public record LoginRequest(string Email, string Senha);

        // =====================================
        // POST: api/v1/auth/register
        // =====================================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { message = "E-mail já cadastrado." });

            var salt = Guid.NewGuid().ToString();
            var senhaHash = ComputeHash(request.Senha, salt);

            // A classe Usuario precisa estar acessível aqui. 
            // Assumo que ela seja a mesma que você definiu abaixo.
            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = senhaHash,
                Salt = salt,
                DataCriacao = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Gera o token de acesso logo após o registro (opcional, mas conveniente)
            var token = _tokenService.GenerateToken(usuario.Id.ToString(), usuario.Email);

            return Ok(new
            {
                message = "Usuário cadastrado com sucesso.",
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email
                }
            });
        }

        // =====================================
        // POST: api/v1/auth/login
        // =====================================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null)
                return Unauthorized(new { message = "Usuário não encontrado." });

            var senhaHash = ComputeHash(request.Senha, usuario.Salt);
            if (senhaHash != usuario.SenhaHash)
                return Unauthorized(new { message = "Senha incorreta." });

            var token = _tokenService.GenerateToken(usuario.Id.ToString(), usuario.Email);

            return Ok(new
            {
                message = "Login efetuado com sucesso!",
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email
                }
            });
        }

        // =====================================
        // Método utilitário para Hash
        // =====================================
        private static string ComputeHash(string senha, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha + salt);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    // =====================================
    // Modelo de usuário (simples)
    // NOTA: Em uma arquitetura limpa, esta classe estaria no projeto Domain.
    // Mantenho-a aqui para que o controlador funcione isoladamente.
    // =====================================
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}

