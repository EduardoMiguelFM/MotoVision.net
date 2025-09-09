using Mottu.Domain.Enums;

namespace Mottu.Domain.ValueObjects
{
    public sealed class SetorCor
    {
        public string Setor { get; }
        public string Cor { get; }
        private SetorCor() { } // EF
        private SetorCor(string setor, string cor) { Setor = setor; Cor = cor; }

        public static SetorCor FromStatus(StatusMoto status) => status switch
        {
            StatusMoto.DISPONIVEL => new("Setor A", "Verde"),
            StatusMoto.RESERVADA => new("Setor B", "Azul"),
            StatusMoto.MANUTENCAO => new("Setor C", "Amarelo"),
            StatusMoto.FALTA_PECA => new("Setor D", "Laranja"),
            StatusMoto.INDISPONIVEL => new("Setor E", "Cinza"),
            StatusMoto.DANOS_ESTRUTURAIS => new("Setor F", "Vermelho"),
            StatusMoto.SINISTRO => new("Setor G", "Preto"),
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
    }
}
