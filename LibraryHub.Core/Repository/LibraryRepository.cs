using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LibraryHub.Core.Repository
{
    internal class LibraryRepository : ILibraryRepository
    {
        private readonly IMongoCollection<LibraryInfo> _libraryCollection;
        private const short KilometerToMeterFactor = 1000;

        public LibraryRepository(MongoContext mongoContext, CancellationToken cancellationToken = default)
        {
            _libraryCollection = mongoContext.GetCollection<LibraryInfo>("Libraries");

            var indexKeys = Builders<LibraryInfo>.IndexKeys.Geo2DSphere(l => l.Location);
            var indexModel = new CreateIndexModel<LibraryInfo>(indexKeys, new CreateIndexOptions { Unique = true });
            _libraryCollection.Indexes.CreateOne(indexModel, cancellationToken: cancellationToken);
        }

        public async Task AddAsync(LibraryInfo library, CancellationToken cancellationToken = default)
        {
            await _libraryCollection.InsertOneAsync(library, cancellationToken: cancellationToken);
        }

        public async Task ImportAsync(List<LibraryInfo> libraries, CancellationToken cancellationToken = default)
        {
            await _libraryCollection.InsertManyAsync(libraries, new InsertManyOptions { IsOrdered = false }, cancellationToken);
        }

        public async Task<List<LibraryInfo>> GetNearbyAsync(double lat, double lng, double radius, CancellationToken cancellationToken = default)
        {
            var radiusInKm = radius * KilometerToMeterFactor;
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                        new GeoJson2DGeographicCoordinates(lng, lat));

            var filter = Builders<LibraryInfo>.Filter.NearSphere(l => l.Location, point, radiusInKm);

            return await _libraryCollection.Find(filter).ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<LibraryInfo>> GetNearbyAsyncv2(double lat, double lng, double radius, CancellationToken cancellationToken = default)
        {
            var readiusInMeters = radius * KilometerToMeterFactor;

            var pipeline = new BsonDocument[]
            {
               new("$geoNear", new BsonDocument
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

            return await _libraryCollection.Aggregate<LibraryInfo>(pipeline, cancellationToken: cancellationToken).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
