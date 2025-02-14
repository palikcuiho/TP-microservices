using BorrowingService.DTO;

namespace BorrowingService.Clients
{
    public interface IUserServiceClient
    {
        Task<bool> UserExistsAndIsNotLockedAsync(int userId);
        Task<UserDTO?> GetUserDetailsAsync(int userId);
    }
}
