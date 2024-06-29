using TravelBuddyApp.Interfaces;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class CreateLogPage : ContentPage
    {
        public CreateLogPage(int tripId, IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();
            BindingContext = new CreateLogViewModel(tripId, apiService, geolocationService);
        }
    }
}
