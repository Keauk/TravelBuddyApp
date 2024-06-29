using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class LandingPage : ContentPage
    {
        private readonly UserResponse _currentUser;
        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;

        public LandingPage(UserResponse user, IApiService apiService, IGeolocationService geolocationService)
        {
            _apiService = apiService;
            _geolocationService = geolocationService;
            _currentUser = user;

            InitializeComponent();

            BindingContext = new LandingPageViewModel(user, apiService);
        }

        private async void OnMakeTripButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MakeTripPage(_currentUser, _apiService, _geolocationService));
        }

        private async void OnMyTripsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyTripsPage(_currentUser, _apiService, _geolocationService));
        }

        private async void OnTripSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                if (e.CurrentSelection[0] is TripResponse selectedTrip)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new TripOverviewPage(selectedTrip, _currentUser.UserId, _apiService, _geolocationService));
                }

                // Deselect the item
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
