﻿using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;

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

        public ICommand LoginCommand { get; }

        private async Task OnLogin()
        {
            var userLogin = new UserLogin
            {
                Username = Username,
                Password = Password
            };

            var success = await _apiService.LoginUserAsync(userLogin);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User logged in successfully!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Login failed.", "OK");
            }
        }
    }
}
