using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class UpdateLogPage : ContentPage
    {
        public UpdateLogPage(TripLogResponse tripLog, IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();
            BindingContext = new UpdateLogViewModel(tripLog, apiService, geolocationService);
        }
    }
}
