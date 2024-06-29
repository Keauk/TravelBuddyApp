using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class MyTripsPage : ContentPage
    {
        private readonly UserResponse _currentUser;
        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;

        public MyTripsPage(UserResponse userResponse, IApiService apiService, IGeolocationService geolocationService)
        {
            _apiService = apiService;
            _geolocationService = geolocationService;
            _currentUser = userResponse;

            InitializeComponent();

            BindingContext = new MyTripsViewModel(userResponse, apiService, geolocationService);
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
