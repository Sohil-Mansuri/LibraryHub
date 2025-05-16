
using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        public Task<List<BookInfo>> GetAllAsync(CancellationToken cancellationToken = default) => bookRepository.GetAllAsync(cancellationToken);

        public Task<BookInfo?> GetByIdAsync(string id, CancellationToken cancellationToken) => bookRepository.GetByIdAsync(id, cancellationToken);

        public Task<List<BookInfo>> FilterAsync(string genre, string author, int? year, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            int skip = (page - 1) * pageSize;
            return bookRepository.FilterAsync(genre, author, year, skip, pageSize, cancellationToken);
        }

        public Task CreateAsync(BookInfo book, CancellationToken cancellationToken) => bookRepository.CreateAsync(book, cancellationToken);

        public Task BulkInsert(List<BookInfo> books, CancellationToken cancellationToken) => bookRepository.BulkInsert(books, cancellationToken);

        public Task UpdateAsync(string id, BookInfo book, CancellationToken cancellationToken) => bookRepository.UpdateAsync(id, book, cancellationToken);

        public Task DeleteAsync(string id, CancellationToken cancellationToken) => bookRepository.DeleteAsync(id, cancellationToken);
    }
}
