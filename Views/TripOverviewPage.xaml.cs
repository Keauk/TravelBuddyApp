using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class TripOverviewPage : ContentPage
    {
        private TripOverviewViewModel _viewModel;

        public TripOverviewPage(TripResponse trip, int currentUserId, IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();
            _viewModel = new TripOverviewViewModel(trip, currentUserId, apiService, geolocationService);
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.LoadLogs();
        }
    }
}
