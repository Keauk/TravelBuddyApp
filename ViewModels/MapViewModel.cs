using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TravelBuddyApp.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        public ICommand ConfirmLocationCommand { get; }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        public MapViewModel()
        {
            ConfirmLocationCommand = new Command(OnConfirmLocation);
        }

        private async void OnConfirmLocation()
        {
            if (SelectedLocation != null)
            {
                // Pass the selected location back to the CreateLogPage
                await Application.Current.MainPage.Navigation.PopAsync();
                MessagingCenter.Send(this, "LocationPicked", SelectedLocation);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No location selected", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
