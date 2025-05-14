using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryHub.Core.Context
{
    internal class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDbSettings> options)
        {
            var clinet = new MongoClient(options.Value.ConnectionString);
            _database = clinet.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) => _database.GetCollection<T>(collectionName);
    }
}
