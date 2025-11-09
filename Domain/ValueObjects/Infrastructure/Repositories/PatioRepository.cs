
using MongoDB.Driver;
using Mottu.Domain.Entities;
using Mottu.Domain.ValueObjects;
using Mottu.Domain.Repositories; // crie a interface no Domain
using Mottu.Infrastructure.Documents;

public class PatioRepository : IPatioRepository
{
    private readonly IMongoCollection<PatioDocument> _collection;
    public PatioRepository(IMongoDatabase db, string collectionName = "patios")
    {
        _collection = db.GetCollection<PatioDocument>(collectionName);
    }

    // Map Patio -> PatioDocument
    private static PatioDocument ToDocument(Patio p)
    {
        return new PatioDocument
        {
            Id = p.Id,
            Nome = p.Nome,
            Endereco = p.Endereco,
            Motos = p.Motos.Select(m => new MotoDocument
            {
                Id = m.Id,
                Modelo = m.Modelo,
                Placa = m.Placa.ToString(),
                Status = m.Status.ToString(),
                SetorCor = m.SetorCor.ToString(),
                PatioId = m.PatioId
            }).ToList()
        };
    }

    // Map PatioDocument -> Patio (reconstituir agregado)
    private static Patio FromDocument(PatioDocument doc)
    {
        var patio = new Patio(doc.Nome, doc.Endereco);
        // precisamos setar o Id privado — usar reflection ou um ctor interno com id.
        // Supondo que você crie um ctor interno: internal Patio(int id, string nome, string endereco) { ... }
        // Aqui vou usar reflection para setar Id e preencher motos.
        var idProp = typeof(Patio).GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        if (idProp != null && idProp.CanWrite) idProp.SetValue(patio, doc.Id);

        foreach (var md in doc.Motos)
        {
            // construir Placa VO a partir da string
            var placaVo = Placa.Parse(md.Placa); // implemente Placa.Parse ou new Placa(md.Placa)
            var moto = patio.AdicionarMoto(md.Modelo, placaVo);
            // setar Id e status de cada moto via reflection ou ctor interno
            var motoIdProp = typeof(Moto).GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (motoIdProp != null && motoIdProp.CanWrite) motoIdProp.SetValue(moto, md.Id);

            // Ajustar status
            moto.DefinirStatus(Enum.Parse<StatusMoto>(md.Status));
            // Set setor cor já será calculado no DefinirStatus
        }

        return patio;
    }

    public async Task CreateAsync(Patio patio)
    {
        var doc = ToDocument(patio);
        await _collection.InsertOneAsync(doc);
    }

    public async Task<Patio?> GetByIdAsync(int id)
    {
        var doc = await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
        return doc == null ? null : FromDocument(doc);
    }

    public async Task<IEnumerable<Patio>> GetAllAsync() =>
        (await _collection.Find(_ => true).ToListAsync()).Select(FromDocument);

    public async Task UpdateAsync(Patio patio) =>
        await _collection.ReplaceOneAsync(d => d.Id == patio.Id, ToDocument(patio));

    public async Task DeleteAsync(int id) =>
        await _collection.DeleteOneAsync(d => d.Id == id);
}
