using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class TripOverviewPage : ContentPage
    {
        public TripOverviewPage(TripResponse trip)
        {
            InitializeComponent();
            BindingContext = new TripOverviewViewModel(trip);
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
