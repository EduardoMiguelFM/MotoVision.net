using Mottu.Domain.Enums;
using Mottu.Domain.ValueObjects;

namespace Mottu.Domain.Entities
{
    public class Moto
    {
        public int Id { get; private set; }
        public string Modelo { get; private set; } = default!;
        public Placa Placa { get; private set; } = default!;
        public StatusMoto Status { get; private set; }
        public SetorCor SetorCor { get; private set; } = default!;
        public int PatioId { get; private set; }
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
            if (novoPatio is null) throw new ArgumentNullException(nameof(novoPatio));
            Patio = novoPatio;
            PatioId = novoPatio.Id;
        }
    }
}