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
    }
}
