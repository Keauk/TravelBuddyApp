using Microsoft.Maui.Controls.Maps;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TravelBuddyApp.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
                UpdatePin();
            }
        }

        private Pin _pin;
        public Pin Pin
        {
            get => _pin;
            private set
            {
                _pin = value;
                OnPropertyChanged();
            }
        }

        public ICommand OnMapClickedCommand { get; }
        public ICommand ConfirmLocationCommand { get; }

        public MapViewModel()
        {
            OnMapClickedCommand = new Command<MapClickedEventArgs>(OnMapClicked);
            ConfirmLocationCommand = new Command(OnConfirmLocation);
        }

        private void OnMapClicked(MapClickedEventArgs e)
        {
            SelectedLocation = new Location(e.Location.Latitude, e.Location.Longitude);
        }

        private async void OnConfirmLocation()
        {
            if (SelectedLocation != null)
            {
                MessagingCenter.Send(this, "LocationPicked", SelectedLocation);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No location selected", "OK");
            }
        }

        private void UpdatePin()
        {
            if (SelectedLocation != null)
            {
                Pin = new Pin
                {
                    Label = "Selected Location",
                    Location = new Location(SelectedLocation.Latitude, SelectedLocation.Longitude)
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
