using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using TravelBuddyApp.Views;

namespace TravelBuddyApp.ViewModels
{
    public class MakeTripViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private const int MaxLines = 6;
        private const int MaxCharacters = 200;

        public MakeTripViewModel()
        {
            _apiService = new ApiService();
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

                await Application.Current.MainPage.Navigation.PushAsync(new TripOverviewPage(createdTrip));
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