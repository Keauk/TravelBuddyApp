using TravelBuddyApp.ViewModels;
using Microsoft.Maui.Controls.Maps;

namespace TravelBuddyApp.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            var viewModel = BindingContext as MapViewModel;
            if (viewModel != null)
            {
                Location location = new(e.Location.Latitude, e.Location.Longitude);
                viewModel.SelectedLocation = location;

                map.Pins.Clear();
                map.Pins.Add(new Pin
                {
                    Label = "Selected Location",
                    Location = location
                });
            }
        }
    }
}
