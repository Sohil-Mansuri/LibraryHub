using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LibraryHub.Core.Entity
{
    public class LibraryInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; } = null!;

        [BsonElement("distanceInMeters")]
        public double DistanceInMeters { get; set; }
    }
}
