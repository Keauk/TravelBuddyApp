using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
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

        private readonly ApiService _apiService;

        public CreateLogViewModel(int tripId)
        {
            TripId = tripId;
            Log = new TripLogInput
            {
                Date = DateTime.Today
            };
            SaveLogCommand = new Command(async () => await OnSaveLog());
            UploadImageCommand = new Command(async () => await OnUploadImage());
            PickLocationCommand = new Command(async () => await OnPickLocation());
            _apiService = new ApiService();

            MessagingCenter.Subscribe<MapViewModel, Services.Location>(this, "LocationPicked", (sender, location) =>
            {
                SelectedLocation = location;
            });
        }

        private Services.Location _selectedLocation;
        public Services.Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        private async Task OnSaveLog()
        {
            if (SelectedLocation != null)
            {
                Log.Location = $"{SelectedLocation.Latitude}, {SelectedLocation.Longitude}";
            }

            HttpResponseMessage response = await _apiService.CreateTripLogAsync(Log, TripId);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Trip log saved successfully!", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save trip log", "OK");
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
            await Application.Current.MainPage.Navigation.PushAsync(new MapPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
