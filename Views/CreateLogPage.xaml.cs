using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class CreateLogPage : ContentPage
    {
        public CreateLogPage(int tripId, IApiService apiService)
        {
            InitializeComponent();
            BindingContext = new CreateLogViewModel(tripId, apiService);
        }
    }
}
