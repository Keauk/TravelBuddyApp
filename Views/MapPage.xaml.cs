using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class MapPage : ContentPage
    {
        private readonly MapViewModel viewModel;

        public MapPage(MapViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;

            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.OnMapClickedCommand.Execute(e);
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MapViewModel.Pin))
            {
                map.Pins.Clear();
                if (viewModel.Pin != null)
                {
                    map.Pins.Add(viewModel.Pin);

                    // Move and zoom the map to 10km above the pin
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(viewModel.Pin.Location.Latitude, viewModel.Pin.Location.Longitude), Distance.FromKilometers(10)));
                }
            }
        }
    }
}
