using Library.Contracts.Book;
using LibraryHub.Core.Entity;

namespace LibraryHub.API.Mapper
{
    public static class BookMapper
    {
        public static Book ToBook(this CreateBookModel createBookDto)
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


        public static Book ToBook(this UpdateBookModel createBookDto)
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

        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Year = book.Year,
                CopiesAvailable = book.CopiesAvailable,
            };
        }
    }
}
