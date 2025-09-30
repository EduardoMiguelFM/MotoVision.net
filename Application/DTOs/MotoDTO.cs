using System.ComponentModel.DataAnnotations;
using Mottu.Domain.Enums;

namespace Mottu.Application.DTOs
{
    /// <summary>
    /// DTO para criação e atualização de motos
    /// </summary>
    public class MotoDto
    {
        /// <summary>
        /// Modelo da moto (ex: Honda Biz, Yamaha Factor)
        /// </summary>
        [Required(ErrorMessage = "Modelo é obrigatório")]
        [StringLength(100, ErrorMessage = "Modelo deve ter no máximo 100 caracteres")]
        public string Modelo { get; set; } = default!;

        /// <summary>
        /// Placa da moto (formato: ABC1234)
        /// </summary>
        [Required(ErrorMessage = "Placa é obrigatória")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Placa deve ter exatamente 7 caracteres")]
        public string Placa { get; set; } = default!;

        /// <summary>
        /// Status da moto
        /// </summary>
        [Required(ErrorMessage = "Status é obrigatório")]
        public StatusMoto Status { get; set; }

        /// <summary>
        /// ID do pátio onde a moto será estacionada
        /// </summary>
        [Required(ErrorMessage = "ID do pátio é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do pátio deve ser maior que zero")]
        public int PatioId { get; set; }
    }

    /// <summary>
    /// DTO para resposta de consultas de motos
    /// </summary>
    public class MotoResponseDto
    {
        /// <summary>
        /// Identificador único da moto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Modelo da moto
        /// </summary>
        public string Modelo { get; set; } = default!;

        /// <summary>
        /// Placa da moto
        /// </summary>
        public string Placa { get; set; } = default!;

        /// <summary>
        /// Status atual da moto
        /// </summary>
        public StatusMoto Status { get; set; }

        /// <summary>
        /// Setor onde a moto está localizada (calculado automaticamente)
        /// </summary>
        public string Setor { get; set; } = default!;

        /// <summary>
        /// Cor do setor (calculada automaticamente)
        /// </summary>
        public string CorSetor { get; set; } = default!;

        /// <summary>
        /// Nome do pátio onde a moto está estacionada
        /// </summary>
        public string NomePatio { get; set; } = default!;
    }
}