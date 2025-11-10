using System.ComponentModel.DataAnnotations;

namespace MotoVision.Application.DTOs
{
    /// <summary>
    /// DTO para criação, atualização e consulta de pátios
    /// </summary>
    public class PatioDto
    {
        /// <summary>
        /// Identificador único do pátio
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do pátio (ex: Pátio Butantã, Pátio Vila Madalena)
        /// </summary>
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(120, ErrorMessage = "Nome deve ter no máximo 120 caracteres")]
        public string Nome { get; set; } = default!;

        /// <summary>
        /// Endereço completo do pátio
        /// </summary>
        [Required(ErrorMessage = "Endereço é obrigatório")]
        [StringLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
        public string Endereco { get; set; } = default!;
    }
}

