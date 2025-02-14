using BorrowingService.DTO;
using System.Net.Http.Json;

namespace BorrowingService.Clients
{
    public class BookServiceClient : IBookServiceClient
    {
        private readonly HttpClient _httpClient;

        public BookServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<bool> BookExistsAndIsAvailableAsync(int bookId)
        {
            if (await GetBookDetailsAsync(bookId) is BookDTO book)
                return book.IsAvailable;
            return false;
        }

        public async Task<BookDTO?> GetBookDetailsAsync(int bookId)
        {
            var response = await _httpClient.GetAsync($"/api/book/{bookId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BookDTO>();
            }
            return null;
        }

        public async Task<bool> ChangeBookAvailabilityAsync(int bookId, bool isAvailable)
        {
            var response = await _httpClient.PutAsJsonAsync<BookDTO>($"/api/book/{bookId}", new() { Id = bookId, IsAvailable = isAvailable});
            return response.IsSuccessStatusCode;
        }
    }
}
