﻿using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class MakeTripPage : ContentPage
    {
        private const int MaxLines = 6;
        private readonly UserResponse _currentUser;

        public MakeTripPage(UserResponse currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            BindingContext = new MakeTripViewModel();
            SetEditorHeight();
            ShowUserAlert();
        }

        private void SetEditorHeight()
        {
            var fontSize = DescriptionEditor.FontSize;
            var height = fontSize * MaxLines;
            DescriptionEditor.HeightRequest = height;
        }

        private async void ShowUserAlert()
        {
            await DisplayAlert("User Info", $"Username: {_currentUser.Username}", "OK");
        }
    }
}