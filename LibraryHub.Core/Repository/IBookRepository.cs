using LibraryHub.Core.Entity;

namespace LibraryHub.Core.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(string id);
        Task<List<Book>> FilterAsync(string genre, string author, int? year, int skip, int take);
        Task CreateAsync(Book book);
        Task BulkInsert(List<Book> books);
        Task UpdateAsync(string id, Book book);
        Task DeleteAsync(string id);
    }
}
