using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LibraryHub.Core.Repository
{
    internal class LibraryRepository : ILibraryRepository
    {
        private readonly IMongoCollection<LibraryInfo> _libraryCollection;
        private const short KilometerToMeterFactor = 1000;

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
            await _libraryCollection.InsertManyAsync(libraries, new InsertManyOptions { IsOrdered = false });
        }

        public async Task<List<LibraryInfo>> GetNearbyLibrariesAsync(double lat, double lng, double radius)
        {
            var radiusInKm = radius * KilometerToMeterFactor;
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                        new GeoJson2DGeographicCoordinates(lng, lat));

            var filter = Builders<LibraryInfo>.Filter.NearSphere(l => l.Location, point, radiusInKm);

            return await _libraryCollection.Find(filter).ToListAsync();
        }

        public async Task<List<LibraryInfo>> GetNearbyLibrariesAsyncV2(double lat, double lng, double radius)
        {
            var readiusInMeters = radius * KilometerToMeterFactor;

            var pipeline = new BsonDocument[]
            {
               new BsonDocument("$geoNear", new BsonDocument
               {
                   { "near", new BsonDocument
                     {
                       { "type", "Point" },
                       {"coordinates", new BsonArray{ lng, lat } }
                     }
                   },
                   { "distanceField", "distanceInMeters" },
                   {"spherical", true },
                   { "maxDistance", readiusInMeters },
                   {"key", "location" }
               })
            };

            return await _libraryCollection.Aggregate<LibraryInfo>(pipeline).ToListAsync();
        }
    }
}
