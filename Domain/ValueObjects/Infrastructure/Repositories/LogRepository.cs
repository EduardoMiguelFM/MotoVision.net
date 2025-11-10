using MongoDB.Driver;
using MotoVision.Application.Interfaces;
using MotoVision.Domain.Entities;

namespace MotoVision.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<MotoLog> _collection;

        public LogRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<MotoLog>("MotoLogs");
        }

        public async Task<IEnumerable<MotoLog>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(MotoLog log)
        {
            await _collection.InsertOneAsync(log);
        }
    }
}

