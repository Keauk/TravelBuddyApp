using System.Net.Http.Json;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7052/")
            };
        }

        public async Task<bool> RegisterUserAsync(UserInput userInput)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", userInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUserAsync(UserLogin userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/login", userLogin);

            return response.IsSuccessStatusCode;
        }
    }
}
