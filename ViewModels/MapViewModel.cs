using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TravelBuddyApp.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        public ICommand ConfirmLocationCommand { get; }

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

        public MapViewModel()
        {
            ConfirmLocationCommand = new Command(OnConfirmLocation);
        }

        private async void OnConfirmLocation()
        {
            if (SelectedLocation != null)
            {
                // Pass the selected location back to the CreateLogPage
                MessagingCenter.Send(this, "LocationPicked", SelectedLocation);
                await Application.Current.MainPage.Navigation.PopAsync();
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
