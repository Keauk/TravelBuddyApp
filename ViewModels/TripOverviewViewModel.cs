using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelBuddyApp.Models;
using TravelBuddyApp.Views;

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
            }
        }

        public ICommand AddTripLogCommand { get; }

        public TripOverviewViewModel(TripResponse trip)
        {
            Trip = trip;
            AddTripLogCommand = new Command(OnAddTripLog);
        }

        private async void OnAddTripLog()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateLogPage(Trip.TripId));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
