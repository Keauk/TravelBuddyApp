using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.Views;

namespace TravelBuddyApp.ViewModels
{
    public class CreateLogViewModel : INotifyPropertyChanged
    {
        private TripLogInput _log;
        public TripLogInput Log
        {
            get => _log;
            set
            {
                _log = value;
                OnPropertyChanged();
            }
        }

        public int TripId { get; private set; }

        public ICommand SaveLogCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand PickLocationCommand { get; }
        public ICommand PickGpsLocationCommand { get; }

        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;

        public CreateLogViewModel(int tripId, IApiService apiService, IGeolocationService geolocationService)
        {
            TripId = tripId;
            Log = new TripLogInput
            {
                Date = DateTime.Today
            };
            SaveLogCommand = new Command(async () => await OnSaveLog());
            UploadImageCommand = new Command(async () => await OnUploadImage());
            PickLocationCommand = new Command(async () => await OnPickLocation());
            PickGpsLocationCommand = new Command(async () => await OnPickGpsLocation());

            _apiService = apiService;
            _geolocationService = geolocationService;

            MessagingCenter.Subscribe<MapViewModel, Location>(this, "LocationPicked", (sender, location) =>
            {
                SelectedLocation = location;
            });
        }

        private Location? _selectedLocation;
        public Location? SelectedLocation
        {
            get => _selectedLocation;
            private set
            {
                _selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }


        public async Task OnSaveLog()
        {
            if (SelectedLocation != null)
            {
                Log.Location = $"{SelectedLocation.Latitude}, {SelectedLocation.Longitude}";
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a location.", "OK");
                return;
            }

            HttpResponseMessage response = await _apiService.CreateTripLogAsync(Log, TripId);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Trip log saved successfully!", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save trip log: {responseBody}", "OK");
            }
        }

        private async Task OnUploadImage()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var photoUrl = await _apiService.UploadPhotoAsync(stream, result.FileName);
                    Log.PhotoPath = $"http://10.0.2.2:5086{photoUrl}";
                    Console.WriteLine($"Photo URL: {Log.PhotoPath}");

                    OnPropertyChanged(nameof(Log));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to pick and upload photo: {ex.Message}", "OK");
            }
        }

        private async Task OnPickLocation()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MapPage(new MapViewModel()));
        }

        private async Task OnPickGpsLocation()
        {
            SelectedLocation = await _geolocationService.GetLastKnownLocationAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
