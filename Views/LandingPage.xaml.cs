using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class LandingPage : ContentPage
    {
        private readonly UserResponse _currentUser;

        public LandingPage(UserResponse user)
        {
            InitializeComponent();
            _currentUser = user;

            BindingContext = new LandingPageViewModel(user);
        }

        private async void OnMakeTripButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MakeTripPage(_currentUser));
        }

        private async void OnMyTripsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTripsPage(_currentUser));
        }
    }
}