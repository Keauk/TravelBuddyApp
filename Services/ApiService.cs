using System.Net.Http.Json;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://yourapiurl.com/");
        }

        public async Task<bool> RegisterUserAsync(UserInput userInput)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", userInput);
            return response.IsSuccessStatusCode;
        }
    }
}
