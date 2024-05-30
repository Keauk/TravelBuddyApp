using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelBuddyApp.Models;

namespace TravelBuddyApp.ViewModels
{
    internal class LandingPageViewModel : INotifyPropertyChanged
    {
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged();
            }
        }

        public LandingPageViewModel(UserResponse user)
        {
            LoadData(user);
        }

        private async void LoadData(UserResponse user)
        {
            // Simulate loading data from a service or database
            await Task.Delay(2000);
            WelcomeMessage = $"Welcome {user.Username} to the Maui Trip Logging App!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}