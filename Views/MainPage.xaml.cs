using TravelBuddyApp.ViewModels; 

namespace TravelBuddyApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
