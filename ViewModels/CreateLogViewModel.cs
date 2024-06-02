using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;

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

        private readonly ApiService _apiService;

        public CreateLogViewModel(int tripId)
        {
            TripId = tripId;
            Log = new TripLogInput();
            SaveLogCommand = new Command(async () => await OnSaveLog());
            UploadImageCommand = new Command(async () => await OnUploadImage());
            _apiService = new ApiService();
        }

        private async Task OnSaveLog()
        {
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
                    Log.PhotoPath = photoUrl;
                    OnPropertyChanged(nameof(Log));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to pick and upload photo: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
