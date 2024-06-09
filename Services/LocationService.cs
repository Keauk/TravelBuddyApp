using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TravelBuddyApp.Services
{
    public class Location : INotifyPropertyChanged
    {
        private double latitude;
        public double Latitude
        {
            get => latitude;
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        private double longitude;
        public double Longitude
        {
            get => longitude;
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class LocationService
    {
        public event Action<Location> LocationSelected;

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                LocationSelected?.Invoke(_selectedLocation);
            }
        }
    }
}
