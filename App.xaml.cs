using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Views;

namespace TravelBuddyApp
{
    public partial class App : Application
    {
        public App(IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(apiService, geolocationService));
        }
    }
}
