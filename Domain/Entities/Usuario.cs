using System.ComponentModel.DataAnnotations;

namespace MotoVision.Domain.Entities
{
    /// <summary>
    /// Representa um usuário do sistema (funcionário da MotoVision)
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string Nome { get; set; } = default!;

        /// <summary>
        /// E-mail do usuário (usado para login)
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Senha do usuário (deve ser criptografada)
        /// </summary>
        public string Senha { get; set; } = default!;

        /// <summary>
        /// CPF do usuário (formato: 123.456.789-00)
        /// </summary>
        public string CPF { get; set; } = default!;

        /// <summary>
        /// Função do usuário no sistema (Administrador, Supervisor, Operador)
        /// </summary>
        public string Funcao { get; set; } = default!;
    }
}

