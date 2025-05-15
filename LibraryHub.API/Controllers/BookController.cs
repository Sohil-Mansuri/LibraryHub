using Library.Contracts.Book;
using LibraryHub.API.Mapper;
using LibraryHub.API.Model;
using LibraryHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.API.Controllers
{
    [Route("api/books/v1")]
    [ApiController]
    public class BookController(BookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(books.Select(b => b.ToBookDto())));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var book = await bookService.GetByIdAsync(id);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));
            return Ok(ApiResponse<BookDto>.Success(book.ToBookDto()));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string genre, int page = 1, int pageSize = 10)
        {
            var books = await bookService.FilterAsync(genre, page, pageSize);
            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(books.Select(b => b.ToBookDto())));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookModel createBookDto)
        {
            await bookService.CreateAsync(createBookDto.ToBook());
            return Ok(ApiResponse<string>.Success("Book created"));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBookModel updateBook)
        {
            var book = await bookService.GetByIdAsync(id);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));

            await bookService.UpdateAsync(id, updateBook.ToBook());
            return Ok(ApiResponse<string>.Success("record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await bookService.GetByIdAsync(id);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));

            await bookService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Success("record deleted"));
        }
    }
}
