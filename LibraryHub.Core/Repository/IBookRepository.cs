using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface IBookRepository
    {
        Task<List<BookInfo>> GetAllAsync();
        Task<BookInfo?> GetByIdAsync(string id);
        Task<List<BookInfo>> FilterAsync(string genre, string author, int? year, int skip, int take);
        Task CreateAsync(BookInfo book);
        Task BulkInsert(List<BookInfo> books);
        Task UpdateAsync(string id, BookInfo book);
        Task DeleteAsync(string id);
    }
}
