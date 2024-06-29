using TravelBuddyApp.Interfaces;
using TravelBuddyApp.Models;
using TravelBuddyApp.ViewModels;

namespace TravelBuddyApp.Views
{
    public partial class MakeTripPage : ContentPage
    {
        private const int MaxLines = 6;

        private readonly IApiService _apiService;
        private readonly IGeolocationService _geolocationService;
        private readonly UserResponse _currentUser;

        public MakeTripPage(UserResponse currentUser, IApiService apiService, IGeolocationService geolocationService)
        {
            _apiService = apiService;
            _currentUser = currentUser;

            InitializeComponent();

            BindingContext = new MakeTripViewModel(currentUser.UserId, apiService, geolocationService);
            SetEditorHeight();
        }

        private void SetEditorHeight()
        {
            var fontSize = DescriptionEditor.FontSize;
            var height = fontSize * MaxLines;
            DescriptionEditor.HeightRequest = height;
        }
    }
}
