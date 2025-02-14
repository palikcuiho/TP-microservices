using BorrowingService.DTO;
using BorrowingService.Models;
using BorrowingService.Services;
using Microsoft.AspNetCore.Mvc;


namespace BorrowingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {

        private readonly IBorrowingsService _borrowingsService;

        public BorrowingController(IBorrowingsService borrowingsService)
        {
            _borrowingsService = borrowingsService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrowing>>> GetBorrowings()
        {
            var borrowings = await _borrowingsService.GetAllBorrowingsAsync();
            return Ok(borrowings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Borrowing>> GetBorrowing(int id)
        {
            var borrowing = await _borrowingsService.GetBorrowingByIdAsync(id);
            if (borrowing == null) return NotFound();
            return Ok(borrowing);
        }

        [HttpPost]
        public async Task<ActionResult> Borrow(BorrowingDTO borrowing)
        {
           if( await _borrowingsService.BorrowBookAsync(borrowing))
            return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowing);
            return Problem();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Return(int id, BorrowingDTO borrowing)
        {
            if (id != borrowing.Id) return BadRequest();
            await _borrowingsService.ReturnBookAsync(borrowing);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBorrowing(int id)
        {
            await _borrowingsService.DeleteBorrowingAsync(id);
            return NoContent();
        }
    }
}

