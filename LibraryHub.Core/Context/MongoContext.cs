using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryHub.Core.Context
{
    internal class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoDbSettings> options, ILogger<MongoContext> logger)
        {
            try
            {
                var client = new MongoClient(options.Value.ConnectionString);
                _database = client.GetDatabase(options.Value.DatabaseName);
            }
            catch (MongoException ex)
            {
                logger.LogError(ex, "Failed to connect to MongoDB using the provided connection string.");
                throw new InvalidOperationException("Unable to establish a connection to MongoDB. Please check your configuration.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while initializing MongoDB.");
                throw;
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) => _database.GetCollection<T>(collectionName);
    }
}
