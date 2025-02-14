using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Mvc;


namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBooksService _booksService;

        public BookController(IBooksService booksService)
        {
            _booksService = booksService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _booksService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            await _booksService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            await _booksService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _booksService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}

