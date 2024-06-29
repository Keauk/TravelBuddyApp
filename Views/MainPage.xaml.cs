using TravelBuddyApp.Interfaces;
using TravelBuddyApp.ViewModels; 

namespace TravelBuddyApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IApiService apiService, IGeolocationService geolocationService)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(apiService, geolocationService);
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is LoginViewModel viewModel)
            {
                await viewModel.LoginCommand.ExecuteAsync(null);
            }
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
