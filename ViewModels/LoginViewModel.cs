using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Net.Http.Json;

namespace TravelBuddyApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public LoginViewModel()
        {
            _apiService = new ApiService();
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

                    await Application.Current.MainPage.Navigation.PushAsync(new Views.LandingPage());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Login failed.", "OK");
            }
        }
    }
}
