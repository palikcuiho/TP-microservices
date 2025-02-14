using BorrowingService.DTO;

namespace BorrowingService.Clients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5002");
        }

        public async Task<bool> UserExistsAndIsNotLockedAsync(int userId)
        {
            if (await GetUserDetailsAsync(userId) is UserDTO user)
                return !user.IsLocked;
            return false;
        }

        public async Task<UserDTO?> GetUserDetailsAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            return null;
        }
    }
}
