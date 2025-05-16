using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using LibraryHub.Core.Utility;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryHub.Core.Repository
{
    internal class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<BookInfo> _booksCollection;

        public BookRepository(MongoContext mongoContext)
        {
            _booksCollection = mongoContext.GetCollection<BookInfo>(Constants.BookCollectionName);
            var indexKeys = Builders<BookInfo>.IndexKeys.Ascending(b => b.Title).Ascending(b => b.Year);
            var indexModel = new CreateIndexModel<BookInfo>(indexKeys, new CreateIndexOptions { Unique = true, Name = "idx_title_year" });
            _booksCollection.Indexes.CreateOne(indexModel);
        }
        public async Task BulkInsert(List<BookInfo> books)
        {
            try
            {
                await _booksCollection.InsertManyAsync(books, new InsertManyOptions { IsOrdered = false });
            }
            catch (MongoBulkWriteException<BookInfo>)
            {
                throw new InvalidOperationException("One or more inserts failed due to duplicate or invalid data.");
            }
        }

        public async Task CreateAsync(BookInfo book)
        {
            try
            {
                await _booksCollection.InsertOneAsync(book);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                // Handle duplicate insert
                throw new InvalidOperationException("A book with the same title and year already exists.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(b => b.Id == id);
        }

        public async Task<List<BookInfo>> FilterAsync(string genre, string author, int? year, int skip, int take)
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

            return await _booksCollection.Aggregate<BookInfo>(pipeline).ToListAsync();
        }

        public async Task<List<BookInfo>> GetAllAsync()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<BookInfo?> GetByIdAsync(string id)
        {
            return await _booksCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, BookInfo book)
        {
            book.Id = id;
            await _booksCollection.ReplaceOneAsync(b => b.Id == id, book, new ReplaceOptions { IsUpsert = true });
        }
    }
}
