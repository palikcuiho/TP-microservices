using BorrowingService.Data;
using BorrowingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingService.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly BorrowingContext _context;

        public BorrowingRepository(BorrowingContext borrowingContext)
        {
            _context = borrowingContext;
        }


        public async Task<bool> AddAsync(Borrowing borrowing)
        {
            await _context.AddAsync(borrowing);
            return await _context.SaveChangesAsync() >0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var borrowing = await _context.Borrowings.FindAsync(id);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteByBookIdAsync(int bookId)
        {
            await _context.Borrowings.Where(b => b.BookId == bookId).ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Borrowing>> GetAllAsync()
        {
           return await _context.Borrowings.ToListAsync();
        }

        public async Task<Borrowing?> GetByIdAsync(int id)
        {
            return await _context.Borrowings.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
