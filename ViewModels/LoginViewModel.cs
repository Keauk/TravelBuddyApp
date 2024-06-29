using CommunityToolkit.Mvvm.Input;
using TravelBuddyApp.Models;
using System.Net.Http.Json;

namespace TravelBuddyApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public LoginViewModel(IApiService apiService)
        {
            _apiService = apiService;
            LoginCommand = new AsyncRelayCommand(OnLogin);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public IAsyncRelayCommand LoginCommand { get; }

        private async Task OnLogin()
        {
            var userLogin = new UserLogin
            {
                Username = Username,
                Password = Password
            };

            var response = await _apiService.LoginUserAsync(userLogin);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (content != null)
                {
                    await SecureStorage.SetAsync("jwt_token", content.Token);
                    await Application.Current.MainPage.DisplayAlert("Success", "User logged in successfully!", "OK");

                    // Get current user info and navigate to LandingPage
                    var user = await _apiService.GetCurrentUserAsync();
                    await Application.Current.MainPage.Navigation.PushAsync(new Views.LandingPage(user, _apiService));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Login failed.", "OK");
            }
        }
    }
}
