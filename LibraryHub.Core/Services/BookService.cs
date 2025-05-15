
using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        public Task<List<Book>> GetAllAsync() => bookRepository.GetAllAsync();

        public Task<Book?> GetByIdAsync(string id) => bookRepository.GetByIdAsync(id);

        public Task<List<Book>> FilterAsync(string genre, string author, int? year, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return bookRepository.FilterAsync(genre, author, year, skip, pageSize);
        }

        public Task CreateAsync(Book book) => bookRepository.CreateAsync(book);

        public Task UpdateAsync(string id, Book book) => bookRepository.UpdateAsync(id, book);

        public Task DeleteAsync(string id) => bookRepository.DeleteAsync(id);
    }
}
