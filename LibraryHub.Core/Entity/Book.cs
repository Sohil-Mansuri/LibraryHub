using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryHub.Core.Entity
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
