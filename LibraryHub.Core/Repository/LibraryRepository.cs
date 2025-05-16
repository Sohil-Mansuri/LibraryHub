using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using MongoDB.Driver;

namespace LibraryHub.Core.Repository
{
    internal class LibraryRepository : ILibraryRepository
    {
        private readonly IMongoCollection<LibraryInfo> _libraryCollection;

        public LibraryRepository(MongoContext mongoContext)
        {
            _libraryCollection = mongoContext.GetCollection<LibraryInfo>("Libraries");

            var indexKeys = Builders<LibraryInfo>.IndexKeys.Geo2DSphere(l => l.Location);
            var indexModel = new CreateIndexModel<LibraryInfo>(indexKeys, new CreateIndexOptions { Unique = true });
            _libraryCollection.Indexes.CreateOne(indexModel);
        }

        public async Task AddLibrary(LibraryInfo library)
        {
            await _libraryCollection.InsertOneAsync(library);
        }

        public async Task ImportLibraries(List<LibraryInfo> libraries)
        {
            await _libraryCollection.InsertManyAsync(libraries);
        }
    }
}
