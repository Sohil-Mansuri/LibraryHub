using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface IBookRepository
    {
        Task<List<BookInfo>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BookInfo?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<BookInfo>> FilterAsync(string genre, string author, int? year, int skip, int take, CancellationToken cancellationToken = default);
        Task CreateAsync(BookInfo book, CancellationToken cancellationToken = default);
        Task BulkInsert(List<BookInfo> books, CancellationToken cancellationToken = default);
        Task UpdateAsync(string id, BookInfo book, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
