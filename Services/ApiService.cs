﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
           
            HttpClientHandler handler = new HttpClientHandler();

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://10.0.2.2:5086/")
            };
        }

        public async Task<HttpResponseMessage> CreateTripAsync(TripInput tripInput)
        {
            await AddJwtTokenAsync();

            string url = "api/trips";

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, tripInput);

            return response;
        }

        public async Task<HttpResponseMessage> CreateTripLogAsync(TripLogInput tripLogInput, int tripId)
        {
            await AddJwtTokenAsync();

            string url = $"api/trips/{tripId}/triplogs";

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, tripLogInput);

            return response;
        }


        public async Task<string> UploadPhotoAsync(Stream photoStream, string fileName)
        {
            await AddJwtTokenAsync();

            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(photoStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            content.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync("api/fileupload/upload", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UploadResult>();
            return result.Url;
        }
        private class UploadResult
        {
            public string Url { get; set; }
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

        public async Task<IEnumerable<TripLogResponse>> GetCurrentLogsForTripAsync(int tripId)
        {
            await AddJwtTokenAsync();

            var url = $"api/trips/{tripId}/triplogs";
            Console.WriteLine(url);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<TripLogResponse>();
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<TripLogResponse>>();
        }

        public async Task<IEnumerable<TripResponse>> GetMyTripsAsync()
        {
            await AddJwtTokenAsync();

            var url = "api/trips/me";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<TripResponse>();
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<TripResponse>>();
        }

        public async Task<IEnumerable<TripResponse>> GetAllTripsAsync()
        {
            await AddJwtTokenAsync();

            var url = "api/trips";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<TripResponse>();
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<TripResponse>>();
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
