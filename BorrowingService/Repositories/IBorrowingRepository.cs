using BorrowingService.Models;
using NuGet.Protocol.Core.Types;

namespace BorrowingService.Repositories
{
    public interface IBorrowingRepository
    {
        Task<IEnumerable<Borrowing>> GetAllAsync();
        Task<Borrowing?> GetByIdAsync(int id);
        Task<bool> AddAsync(Borrowing borrowing);
        Task<bool> UpdateAsync(Borrowing borrowing);
        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteByBookIdAsync(int bookId);

    }
}
