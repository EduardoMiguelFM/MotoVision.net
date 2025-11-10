using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MotoVision.Domain.Enums;

namespace MotoVision.Domain.Entities
{
    public class MotoLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int MotoId { get; set; }
        public StatusMoto StatusAnterior { get; set; }
        public StatusMoto StatusAtual { get; set; }
        public DateTime DataAlteracao { get; set; } = DateTime.UtcNow;
    }
}

