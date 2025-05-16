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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var books = await bookService.GetAllAsync(cancellationToken);
            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(books.Select(b => b.ToBookDto())));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var book = await bookService.GetByIdAsync(id, cancellationToken);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));
            return Ok(ApiResponse<BookDto>.Success(book.ToBookDto()));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string? genre, string? author, int? year, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var books = await bookService.FilterAsync(genre!, author!, year, page, pageSize, cancellationToken);
            return Ok(ApiResponse<IEnumerable<BookDto>>.Success(books.Select(b => b.ToBookDto())));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookModel book, CancellationToken cancellationToken)
        {
            await bookService.CreateAsync(book.ToBook(), cancellationToken);
            return Ok(ApiResponse<string>.Success("Book created"));
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportBooks(List<CreateBookModel> books, CancellationToken cancellationToken)
        {
            await bookService.BulkInsert([.. books.Select(b => b.ToBook())], cancellationToken);
            return Ok(ApiResponse<string>.Success("books imported successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBookModel updateBook, CancellationToken cancellationToken)
        {
            var book = await bookService.GetByIdAsync(id, cancellationToken);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));

            await bookService.UpdateAsync(id, updateBook.ToBook(), cancellationToken);
            return Ok(ApiResponse<string>.Success("record updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var book = await bookService.GetByIdAsync(id, cancellationToken);
            if (book is null) return NotFound(ApiResponse<string>.Fail(new ErrorResponse(message: "Book not found", errorCode: "404")));

            await bookService.DeleteAsync(id, cancellationToken: cancellationToken);
            return Ok(ApiResponse<string>.Success("record deleted"));
        }
    }
}
