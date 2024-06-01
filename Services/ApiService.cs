using System.Net.Http.Headers;
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

        public async Task<HttpResponseMessage> CreateTripAsync(TripInput tripInput)
        {
            await AddJwtTokenAsync();
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/trips", tripInput);

            return response;
        }

        public async Task<HttpResponseMessage> RegisterUserAsync(UserInput userInput)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", userInput);

            return response;
        }

        public async Task<HttpResponseMessage> LoginUserAsync(UserLogin userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/login", userLogin);

            return response;
        }

        public async Task<UserResponse> GetCurrentUserAsync()
        {
            await AddJwtTokenAsync();
            var response = await _httpClient.GetFromJsonAsync<UserResponse>("api/users/me");

            return response;
        }

        private async Task AddJwtTokenAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
