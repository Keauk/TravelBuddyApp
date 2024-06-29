using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class TripOverviewPage : ContentPage
    {
        public TripOverviewPage(TripResponse trip, int currentUserId, IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();
            BindingContext = new TripOverviewViewModel(trip, currentUserId, apiService, geolocationService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is TripOverviewViewModel viewModel)
            {
                viewModel.LoadLogs();
            }
        }
    }
}
