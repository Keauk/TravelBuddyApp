using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using TravelBuddyApp.Views;
using System.Diagnostics;

namespace TravelBuddyApp.ViewModels
{
    public class TripOverviewViewModel : INotifyPropertyChanged
    {
        private TripResponse _trip;
        public TripResponse Trip
        {
            get => _trip;
            set
            {
                _trip = value;
                OnPropertyChanged();
                LoadLogs();
            }
        }

        private ObservableCollection<TripLogResponse> _logs;
        public ObservableCollection<TripLogResponse> Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTripLogCommand { get; }

        private readonly ApiService _apiService;
        private int _currentUserId;

        public TripOverviewViewModel(int currentUserId)
        {
            _apiService = new ApiService();
            _currentUserId = currentUserId;
            Logs = new ObservableCollection<TripLogResponse>();
            AddTripLogCommand = new Command(OnAddTripLog);
        }

        public TripOverviewViewModel(TripResponse trip, int currentUserId) : this(currentUserId)
        {
            Trip = trip;
        }

        public async void LoadLogs()
        {
            if (Trip != null)
            {
                try
                {
                    var logs = await _apiService.GetCurrentLogsForTripAsync(Trip.TripId);
                    Logs.Clear();
                    foreach (var log in logs)
                    {
                        Logs.Add(log);
                    }
                    Debug.WriteLine($"Logs loaded: {Logs.Count} items");
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine($"HTTP Request Exception: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to load logs. Please check your network connection.", "OK");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    await Application.Current.MainPage.DisplayAlert("Error", "An unexpected error occurred.", "OK");
                }
            }
        }

        private async void OnAddTripLog()
        {
            if (Trip != null)
            {
                if (Trip.UserId == _currentUserId)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CreateLogPage(Trip.TripId, _apiService));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Unauthorized", "You do not have permission to add logs to this trip.", "OK");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
