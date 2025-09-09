using Mottu.Domain.ValueObjects;

namespace Mottu.Domain.Entities
{
    public class Patio
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = default!;
        private readonly List<Moto> _motos = new();
        public IReadOnlyCollection<Moto> Motos => _motos;

        private Patio() { } // EF
        public Patio(string nome) => Nome = nome;

        public Moto AdicionarMoto(string modelo, Placa placa)
        {
            if (_motos.Any(m => m.Placa.Equals(placa)))
                throw new InvalidOperationException("Já existe moto com essa placa no pátio.");
            var moto = new Moto(modelo, placa, this);
            _motos.Add(moto);
            return moto;
        }

        public void RemoverMoto(int motoId)
        {
            var moto = _motos.FirstOrDefault(x => x.Id == motoId);
            if (moto is null) throw new KeyNotFoundException("Moto não encontrada.");
            _motos.Remove(moto);
        }
    }
}