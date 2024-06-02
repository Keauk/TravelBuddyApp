﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using System.Diagnostics;
using TravelBuddyApp.Views;

namespace TravelBuddyApp.ViewModels
{
    public class MyTripsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TripResponse> _trips;
        public ObservableCollection<TripResponse> Trips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTripCommand { get; }

        private readonly ApiService _apiService;
        private readonly UserResponse _user;

        public MyTripsViewModel(UserResponse userResponse)
        {
            _apiService = new ApiService();
            _user = userResponse;
            Trips = new ObservableCollection<TripResponse>();
            AddTripCommand = new Command(OnAddTrip);
            LoadTrips();
        }

        public async void LoadTrips()
        {
            try
            {
                var trips = await _apiService.GetMyTripsAsync();
                Trips.Clear();
                foreach (var trip in trips)
                {
                    Trips.Add(trip);
                }
                Debug.WriteLine($"Trips loaded: {Trips.Count} items");
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"HTTP Request Exception: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load trips. Please check your network connection.", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred.", "OK");
            }
        }

        private async void OnAddTrip()
        {
            // Navigate to the page to create a new trip
            await Application.Current.MainPage.Navigation.PushAsync(new MakeTripPage(_user));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
