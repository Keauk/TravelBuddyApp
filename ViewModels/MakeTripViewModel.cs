using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.Views;

namespace TravelBuddyApp.ViewModels
{
    public class MakeTripViewModel : INotifyPropertyChanged
    {
        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;

        private readonly int _currentUserId;
        private const int MaxLines = 6;
        private const int MaxCharacters = 200;

        public MakeTripViewModel(int currentUserId, IApiService apiService, IGeolocationService geolocationService)
        {
            _apiService = apiService;
            _geolocationService = geolocationService;

            _currentUserId = currentUserId;
            CreateTripCommand = new AsyncRelayCommand(CreateTrip);
        }

        private string _tripName;
        public string TripName
        {
            get => _tripName;
            set
            {
                _tripName = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = LimitText(value);
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CreateTripCommand { get; }

        private async Task CreateTrip()
        {
            var tripInput = new TripInput
            {
                Title = TripName,
                Description = Description
            };

            var response = await _apiService.CreateTripAsync(tripInput);
            if (response.IsSuccessStatusCode)
            {
                var createdTrip = await response.Content.ReadFromJsonAsync<TripResponse>();

                await Application.Current.MainPage.DisplayAlert("Success", "Trip created successfully!", "OK");

                await Application.Current.MainPage.Navigation.PushAsync(new TripOverviewPage(createdTrip, _currentUserId, _apiService, _geolocationService));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Trip creation failed", "OK");
            }
        }

        private string LimitText(string text)
        {
            var lines = text.Split('\n');
            if (lines.Length > MaxLines)
            {
                text = string.Join("\n", lines.Take(MaxLines));
            }

            if (text.Length > MaxCharacters)
            {
                text = text.Substring(0, MaxCharacters);
            }

            return text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
