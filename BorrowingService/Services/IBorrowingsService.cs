using BorrowingService.DTO;

namespace BorrowingService.Services
{
    public interface IBorrowingsService
    {
        Task<IEnumerable<BorrowingDTO>> GetAllBorrowingsAsync();
        Task<BorrowingDTO?> GetBorrowingByIdAsync(int id);
        Task<bool> BorrowBookAsync(BorrowingDTO borrowing);
        Task<bool> ReturnBookAsync(BorrowingDTO borrowing);
        Task<bool> UpdateBorrowingAsync(BorrowingDTO borrowing);
        Task DeleteBorrowingAsync(int id);

        Task<bool> DeleteBorrowingsByBookIdAsync(int bookId);


    }
}
