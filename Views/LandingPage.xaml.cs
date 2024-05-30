using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage(UserResponse user)
        {
            InitializeComponent();
            BindingContext = new LandingPageViewModel(user);
        }
    }
}