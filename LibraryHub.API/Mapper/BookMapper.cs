using Library.Contracts.Book;
using LibraryHub.Core.Entity;

namespace LibraryHub.API.Mapper
{
    public static class BookMapper
    {
        public static Book ToBook(this CreateBook createBookDto)
        {
            return new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                CopiesAvailable = createBookDto.CopiesAvailable,
                Genre = createBookDto.Genre,
                Year = createBookDto.Year,
            };
        }


        public static Book ToBook(this UpdateBook createBookDto)
        {
            return new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                CopiesAvailable = createBookDto.CopiesAvailable,
                Genre = createBookDto.Genre,
                Year = createBookDto.Year,
            };
        }
    }
}
