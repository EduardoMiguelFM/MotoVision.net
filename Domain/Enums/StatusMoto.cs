using System.ComponentModel;

namespace MotoVision.Domain.Enums
{
    /// <summary>
    /// Status possíveis para uma moto no sistema
    /// </summary>
    public enum StatusMoto
    {
        /// <summary>
        /// Moto disponível para locação (Setor A - Verde)
        /// </summary>
        [Description("Disponível para locação")]
        DISPONIVEL,

        /// <summary>
        /// Moto reservada por um cliente (Setor B - Azul)
        /// </summary>
        [Description("Reservada por cliente")]
        RESERVADA,

        /// <summary>
        /// Moto em manutenção (Setor C - Amarelo)
        /// </summary>
        [Description("Em manutenção")]
        MANUTENCAO,

        /// <summary>
        /// Moto aguardando peças para manutenção (Setor D - Laranja)
        /// </summary>
        [Description("Aguardando peças")]
        FALTA_PECA,

        /// <summary>
        /// Moto temporariamente indisponível (Setor E - Cinza)
        /// </summary>
        [Description("Indisponível")]
        INDISPONIVEL,

        /// <summary>
        /// Moto com danos estruturais (Setor F - Vermelho)
        /// </summary>
        [Description("Danos estruturais")]
        DANOS_ESTRUTURAIS,

        /// <summary>
        /// Moto envolvida em sinistro (Setor G - Preto)
        /// </summary>
        [Description("Sinistro")]
        SINISTRO
    }
}

