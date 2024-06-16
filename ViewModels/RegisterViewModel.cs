using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using Microsoft.Maui.Controls;

namespace TravelBuddyApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public RegisterViewModel()
        {
            _apiService = new ApiService();
            RegisterCommand = new AsyncRelayCommand(OnRegister);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand RegisterCommand { get; }

        private async Task OnRegister()
        {
            var userInput = new UserInput
            {
                Username = Username,
                Email = Email,
                Password = Password
            };

            HttpResponseMessage response = await _apiService.RegisterUserAsync(userInput);
            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User registered successfully!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User registration failed.", "OK");
            }
        }
    }
}
