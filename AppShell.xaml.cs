using Android.Content.Res;
using TravelBuddyApp.Views;

namespace TravelBuddyApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LandingPage), typeof(LandingPage));
        }
    }
}
