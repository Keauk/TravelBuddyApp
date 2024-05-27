namespace TravelBuddyApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
