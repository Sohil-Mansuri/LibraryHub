using Library.Contracts.Book;
using LibraryHub.API.Mapper;
using LibraryHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.API.Controllers
{
    [Route("api/books/v1/")]
    [ApiController]
    public class BookController(BookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await bookService.GetAllAsync();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var book = await bookService.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string genre, int page = 1, int pageSize = 10)
        {
            var books = await bookService.FilterAsync(genre, page, pageSize);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBook createBookDto)
        {
            await bookService.CreateAsync(createBookDto.ToBook());
            return CreatedAtAction(nameof(GetById), new { id = "" });
        }


        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateBook updateBook)
        {
            var book = await bookService.GetByIdAsync(id);
            if (book is null) return NotFound(id);

            await bookService.UpdateAsync(id, updateBook.ToBook());
            return Ok("updated successfully");
        }
    }
}
