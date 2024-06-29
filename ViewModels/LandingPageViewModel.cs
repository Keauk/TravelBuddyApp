using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.ViewModels
{
    internal class LandingPageViewModel : INotifyPropertyChanged
    {

        private readonly IApiService _apiService;
        private readonly UserResponse _user;

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged();
            }
        }

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

        public LandingPageViewModel(UserResponse user, IApiService apiService)
        {
            LoadData(user);
            _apiService = apiService;
            Trips = new ObservableCollection<TripResponse>();
            LoadTrips();
        }

        public async void LoadTrips()
        {
            try
            {
                var trips = await _apiService.GetAllTripsAsync();
                Trips.Clear();
                foreach (var trip in trips)
                {
                    Trips.Add(trip);
                }
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load trips. Please check your network connection.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred.", "OK");
            }
        }

        private async void LoadData(UserResponse user)
        {
            await Task.Delay(1000);
            WelcomeMessage = $"Welcome to TravelBuddy, {user.Username}!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}