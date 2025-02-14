using BorrowingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingService.Data
{
    public class BorrowingContext : DbContext
    {
        public BorrowingContext(DbContextOptions<BorrowingContext> options): base(options) { }
        public DbSet<Borrowing> Borrowings { get; set; }
    }
}
