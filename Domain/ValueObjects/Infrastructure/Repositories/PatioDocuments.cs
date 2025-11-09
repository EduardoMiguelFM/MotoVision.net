
using MongoDB.Bson.Serialization.Attributes;

namespace Mottu.Infrastructure.Documents
{
    public class PatioDocument
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Endereco { get; set; } = "";
        public List<MotoDocument> Motos { get; set; } = new();
    }

    public class MotoDocument
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = "";
        public string Placa { get; set; } = ""; // Placa VO armazenada como string
        public string Status { get; set; } = ""; // serializar enum como string
        public string SetorCor { get; set; } = "";
        public int PatioId { get; set; }
    }
}
