using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class CreateLogPage : ContentPage
    {
        public CreateLogPage(int tripId)
        {
            InitializeComponent();
            BindingContext = new CreateLogViewModel(tripId);
        }
    }
}
