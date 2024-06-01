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
            await Task.Delay(1000);
            WelcomeMessage = $"Welcome to TravelBuddy, {user.Username}!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}