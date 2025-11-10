using System;
using System.ComponentModel.DataAnnotations;
using MotoVision.Domain.Enums;
using MotoVision.Domain.ValueObjects;



namespace MotoVision.Domain.Entities
{
    /// <summary>
    /// Representa uma moto no sistema de compartilhamento da MotoVision
    /// </summary>
    public class Moto
    {
        /// <summary>
        /// Identificador único da moto
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Modelo da moto (ex: Honda Biz, Yamaha Factor)
        /// </summary>
        public string Modelo { get; private set; } = default!;

        /// <summary>
        /// Placa da moto (formato: ABC1234)
        /// </summary>
        public Placa Placa { get; private set; } = default!;

        /// <summary>
        /// Status atual da moto (DISPONIVEL, RESERVADA, MANUTENCAO, etc.)
        /// </summary>
        public StatusMoto Status { get; private set; }

        /// <summary>
        /// Setor e cor onde a moto está localizada (calculado automaticamente baseado no status)
        /// </summary>
        public SetorCor SetorCor { get; private set; } = default!;

        /// <summary>
        /// ID do pátio onde a moto está localizada
        /// </summary>
        public int PatioId { get; private set; }

        /// <summary>
        /// Pátio onde a moto está localizada
        /// </summary>
        public Patio Patio { get; private set; } = default!;

        private Moto() { } // EF

        internal Moto(string modelo, Placa placa, Patio patio)
        {
            Modelo = modelo;
            Placa = placa;
            Patio = patio;
            PatioId = patio.Id;
            DefinirStatus(StatusMoto.DISPONIVEL);
        }

        public void DefinirStatus(StatusMoto novoStatus)
        {
            if (Status == StatusMoto.SINISTRO && novoStatus != StatusMoto.SINISTRO)
                throw new InvalidOperationException("Moto em SINISTRO não pode mudar de status.");

            Status = novoStatus;
            SetorCor = SetorCor.FromStatus(novoStatus);
        }

        public void MoverPara(Patio novoPatio)
        {
            if (novoPatio is null)
                throw new ArgumentNullException(nameof(novoPatio));

            Patio = novoPatio;
            PatioId = novoPatio.Id;
        }
    }
}

