namespace Mottu.Domain.ValueObjects
{
    public sealed class Placa : IEquatable<Placa>
    {
        public string Valor { get; }
        private Placa() { } // EF
        public Placa(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) || valor.Length != 7)
                throw new ArgumentException("Placa inválida.");
            Valor = valor.ToUpperInvariant();
        }
        public bool Equals(Placa? other) => other is not null && Valor == other.Valor;
        public override bool Equals(object? obj) => Equals(obj as Placa);
        public override int GetHashCode() => Valor.GetHashCode();
        public override string ToString() => Valor;
        public static implicit operator string(Placa p) => p.Valor;
    }
}