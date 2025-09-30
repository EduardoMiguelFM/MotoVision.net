using System.ComponentModel.DataAnnotations;
using Mottu.Domain.ValueObjects;

namespace Mottu.Domain.Entities
{
    /// <summary>
    /// Representa um pátio onde as motos ficam estacionadas
    /// </summary>
    public class Patio
    {
        /// <summary>
        /// Identificador único do pátio
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Nome do pátio (ex: Pátio Butantã, Pátio Vila Madalena)
        /// </summary>
        public string Nome { get; private set; } = default!;

        /// <summary>
        /// Endereço completo do pátio
        /// </summary>
        public string Endereco { get; private set; } = default!;

        private readonly List<Moto> _motos = new();

        /// <summary>
        /// Lista de motos estacionadas neste pátio
        /// </summary>
        public IReadOnlyCollection<Moto> Motos => _motos;

        private Patio() { } // EF
        public Patio(string nome, string endereco)
        {
            Nome = nome;
            Endereco = endereco;
        }

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