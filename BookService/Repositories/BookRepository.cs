using BookService.Data;
using BookService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookService.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext bookContext)
        {
            _context = bookContext;
        }


        public async Task AddAsync(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
           return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
           await _context.SaveChangesAsync();
        }
    }
}
