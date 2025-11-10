using System.ComponentModel.DataAnnotations;

namespace MotoVision.Application.DTOs
{
    /// <summary>
    /// DTO para criação, atualização e consulta de usuários
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador único do usuário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(120, ErrorMessage = "Nome deve ter no máximo 120 caracteres")]
        public string Nome { get; set; } = default!;

        /// <summary>
        /// E-mail do usuário (usado para login)
        /// </summary>
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail deve ter um formato válido")]
        [StringLength(200, ErrorMessage = "E-mail deve ter no máximo 200 caracteres")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Senha do usuário (deve ser criptografada)
        /// </summary>
        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; set; } = default!;

        /// <summary>
        /// CPF do usuário (formato: 123.456.789-00)
        /// </summary>
        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF deve ter entre 11 e 14 caracteres")]
        public string CPF { get; set; } = default!;

        /// <summary>
        /// Função do usuário no sistema (Administrador, Supervisor, Operador)
        /// </summary>
        [Required(ErrorMessage = "Função é obrigatória")]
        [StringLength(60, ErrorMessage = "Função deve ter no máximo 60 caracteres")]
        public string Funcao { get; set; } = default!;
    }
}

