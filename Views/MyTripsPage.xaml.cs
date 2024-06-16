﻿using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class MyTripsPage : ContentPage
    {
        private readonly UserResponse _currentUser;

        public MyTripsPage(UserResponse userResponse)
        {
            InitializeComponent();
            _currentUser = userResponse;
            BindingContext = new MyTripsViewModel(userResponse);
        }

        private async void OnTripSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                if (e.CurrentSelection[0] is TripResponse selectedTrip)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TripOverviewPage(selectedTrip, _currentUser.UserId));
                }

                // Deselect the item
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
