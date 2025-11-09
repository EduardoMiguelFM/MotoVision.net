
using MongoDB.Driver;
using Mottu.Infrastructure.Documents;
using Mottu.Domain.Entities;

public class MotoRepository : IMotoRepository
{
    private readonly IMongoCollection<MotoDocument> _collection;
    public MotoRepository(IMongoDatabase db, string collectionName = "motos")
    {
        _collection = db.GetCollection<MotoDocument>(collectionName);
    }

    private static Moto ToEntity(MotoDocument d)
    {
        var placa = Placa.Parse(d.Placa);
        var patio = new Patio("", ""); // placeholder: se precisar, carregar Patio via PatioRepository
        var moto = /* new Moto with internal ctor if available */ new Moto(d.Modelo, placa, patio);
        // set Id, Status, PatioId appropriately (use internal ctor with id or reflection)
        return moto;
    }

    // Implement Create/Get/Update/Delete similar ao PatioRepository
}
