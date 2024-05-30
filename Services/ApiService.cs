using System.Net.Http.Json;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
           
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7052/")
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
