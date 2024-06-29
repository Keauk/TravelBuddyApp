using TravelBuddyApp.Views;

namespace TravelBuddyApp
{
    public partial class App : Application
    {
        public App(IApiService apiService)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(apiService));
        }
    }
}
