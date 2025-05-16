
using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        public Task<List<BookInfo>> GetAllAsync() => bookRepository.GetAllAsync();

        public Task<BookInfo?> GetByIdAsync(string id) => bookRepository.GetByIdAsync(id);

        public Task<List<BookInfo>> FilterAsync(string genre, string author, int? year, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return bookRepository.FilterAsync(genre, author, year, skip, pageSize);
        }

        public Task CreateAsync(BookInfo book) => bookRepository.CreateAsync(book);

        public Task BulkInsert(List<BookInfo> books) => bookRepository.BulkInsert(books);

        public Task UpdateAsync(string id, BookInfo book) => bookRepository.UpdateAsync(id, book);

        public Task DeleteAsync(string id) => bookRepository.DeleteAsync(id);
    }
}
