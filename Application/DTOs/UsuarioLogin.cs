using System.ComponentModel.DataAnnotations;

namespace MotoVision.Application.DTOs
{
    /// <summary>
    /// DTO usado para autenticação de usuário (login)
    /// </summary>
    public class UsuarioLoginDto
    {
        /// <summary>
        /// E-mail do usuário utilizado para login
        /// </summary>
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail deve ter um formato válido")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário utilizada para autenticação
        /// </summary>
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }
}

