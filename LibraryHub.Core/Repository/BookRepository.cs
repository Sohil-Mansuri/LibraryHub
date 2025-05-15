using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using LibraryHub.Core.Utility;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryHub.Core.Repository
{
    internal class BookRepository(MongoContext mongoContext) : IBookRepository
    {
        private readonly IMongoCollection<Book> _booksCollection = mongoContext.GetCollection<Book>(Constants.BookCollectionName);

        public async Task CreateAsync(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
        }

        public async Task DeleteAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(b => b.Id == id);
        }

        public async Task<List<Book>> FilterAsync(string genre, string author, int? year, int skip, int take)
        {
            var matchConditions = new BsonDocument();

            if (!string.IsNullOrEmpty(genre))
                matchConditions.Add("Genre", genre);

            if (!string.IsNullOrEmpty(author))
                matchConditions.Add("Author", new BsonDocument("$regex", author).Add("$options", "i"));

            if (year is not null)
                matchConditions.Add("Year", year);

            var pipeline = new List<BsonDocument>();

            if (matchConditions.ElementCount > 0)
                pipeline.Add(new BsonDocument("$match", matchConditions));

            pipeline.Add(new BsonDocument("$skip", skip));
            pipeline.Add(new BsonDocument("$limit", take));

            return await _booksCollection.Aggregate<Book>(pipeline).ToListAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(string id)
        {
            return await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Book book)
        {
            book.Id = id;
            await _booksCollection.ReplaceOneAsync(b => b.Id == id, book, new ReplaceOptions { IsUpsert = true });
        }
    }
}
