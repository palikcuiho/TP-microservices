using BorrowingService.DTO;

namespace BorrowingService.Clients
{
    public interface IBookServiceClient
    {
        Task<bool> BookExistsAndIsAvailableAsync(int bookId);
        Task<BookDTO?> GetBookDetailsAsync(int bookId);
        Task<bool> ChangeBookAvailabilityAsync(int bookId, bool isAvailable);
    }
}
