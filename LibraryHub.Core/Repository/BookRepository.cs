using LibraryHub.Core.Context;
using LibraryHub.Core.Entity;
using MongoDB.Driver;

namespace LibraryHub.Core.Repository
{
    internal class BookRepository(MongoContext mongoContext) : IBookRepository
    {
        private readonly IMongoCollection<Book> _booksCollection = mongoContext.GetCollection<Book>("Books");

        public async Task CreateAsync(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
        }

        public async Task DeleteAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(id);
        }

        public async Task<List<Book>> FilterAsync(string genre, int skip, int take)
        {
            return await _booksCollection.Find(b => b.Genre == genre).
                Skip(skip).
                Limit(take).
                ToListAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(string id)
        {
            return await _booksCollection.Find(id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Book book)
        {
            await _booksCollection.ReplaceOneAsync(id, book);
        }
    }
}
