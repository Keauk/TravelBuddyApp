using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Views;
using System.Diagnostics;
using TravelBuddyApp.Interfaces;

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

        private TripLogResponse _selectedTripLog;
        public TripLogResponse SelectedTripLog
        {
            get => _selectedTripLog;
            set
            {
                _selectedTripLog = value;
                OnPropertyChanged();
                if (_selectedTripLog != null)
                {
                    OnClickTripLog(_selectedTripLog);
                    SelectedTripLog = null;
                }
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
        public ICommand UpdateTripLogCommand { get; }

        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;

        private int _currentUserId;

        public TripOverviewViewModel(TripResponse trip, int currentUserId, IApiService apiService, IGeolocationService geolocationService)
        {
            _apiService = apiService;
            _geolocationService = geolocationService;

            _currentUserId = currentUserId;
            Logs = new ObservableCollection<TripLogResponse>();
            AddTripLogCommand = new Command(OnAddTripLog);
            UpdateTripLogCommand = new Command<TripLogResponse>(async (log) => await OnClickTripLog(log));
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
            if (Trip != null && Trip.UserId == _currentUserId)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CreateLogPage(Trip.TripId, _apiService, _geolocationService));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Unauthorized", "You do not have permission to add logs to this trip.", "OK");
            }
        }

        public async Task OnClickTripLog(TripLogResponse tripLog)
        {
            if (Trip != null && Trip.UserId == _currentUserId)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new UpdateLogPage(tripLog, _apiService, _geolocationService));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Unauthorized", "You do not have permission to edit this trip log.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
