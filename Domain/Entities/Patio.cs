using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MotoVision.Domain.ValueObjects;

namespace MotoVision.Domain.Entities
{
    /// <summary>
    /// Representa um pátio onde as motos ficam estacionadas.
    /// </summary>
    public class Patio
    {
        /// <summary>
        /// Identificador único do pátio.
        /// </summary>
        [Key]
        public int Id { get; private set; }

        /// <summary>
        /// Nome do pátio (ex: Pátio Butantã, Pátio Vila Madalena).
        /// </summary>
        [Required]
        public string Nome { get; private set; } = string.Empty;

        /// <summary>
        /// Endereço completo do pátio.
        /// </summary>
        [Required]
        public string Endereco { get; private set; } = string.Empty;

        private readonly List<Moto> _motos = new();

        /// <summary>
        /// Lista de motos estacionadas neste pátio.
        /// </summary>
        public IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

        private Patio() { } // Construtor protegido para EF

        public Patio(string nome, string endereco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do pátio é obrigatório.", nameof(nome));

            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Endereço do pátio é obrigatório.", nameof(endereco));

            Nome = nome;
            Endereco = endereco;
        }

        /// <summary>
        /// Adiciona uma moto ao pátio, garantindo unicidade pela placa.
        /// </summary>
        public Moto AdicionarMoto(string modelo, Placa placa)
        {
            if (_motos.Any(m => m.Placa.Equals(placa)))
                throw new InvalidOperationException("Já existe uma moto com essa placa no pátio.");

            var moto = new Moto(modelo, placa, this);
            _motos.Add(moto);
            return moto;
        }

        /// <summary>
        /// Remove uma moto do pátio pelo identificador.
        /// </summary>
        public void RemoverMoto(int motoId)
        {
            var moto = _motos.FirstOrDefault(x => x.Id == motoId);
            if (moto is null)
                throw new KeyNotFoundException("Moto não encontrada.");

            _motos.Remove(moto);
        }
    }
}
