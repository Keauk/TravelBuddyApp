using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TravelBuddyApp.Models;
using TravelBuddyApp.Services;
using TravelBuddyApp.ViewModels;
using Xunit;

namespace TravelBuddyApp.UnitTests
{
    public class CreateLogViewModelTests
    {
        private readonly Mock<ApiService> _apiServiceMock;
        private readonly CreateLogViewModel _viewModel;

        public CreateLogViewModelTests()
        {
            _apiServiceMock = new Mock<ApiService>();
            _viewModel = new CreateLogViewModel(1);
        }

        [Fact]
        public async Task OnSaveLog_SuccessfulSave_DisplaysSuccessMessage()
        {
            // Arrange
            _viewModel.Log.Location = "Test Location";
            _viewModel.Log.Notes = "Test Notes";
            _viewModel.Log.PhotoPath = "path/to/photo.jpg";
            _apiServiceMock.Setup(s => s.CreateTripLogAsync(It.IsAny<TripLogInput>(), It.IsAny<int>()))
                           .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            // Act
            await _viewModel.OnSaveLog();

            // Assert
            _apiServiceMock.Verify(s => s.CreateTripLogAsync(It.Is<TripLogInput>(log => log.Location == "Test Location"), It.IsAny<int>()), Times.Once);
            // Add additional assertions as needed to verify that the success message was displayed
        }

        [Fact]
        public async Task OnSaveLog_FailedSave_DisplaysErrorMessage()
        {
            // Arrange
            _viewModel.Log.Location = "Test Location";
            _viewModel.Log.Notes = "Test Notes";
            _viewModel.Log.PhotoPath = "path/to/photo.jpg";
            _apiServiceMock.Setup(s => s.CreateTripLogAsync(It.IsAny<TripLogInput>(), It.IsAny<int>()))
                           .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            // Act
            await _viewModel.OnSaveLog();

            // Assert
            _apiServiceMock.Verify(s => s.CreateTripLogAsync(It.Is<TripLogInput>(log => log.Location == "Test Location"), It.IsAny<int>()), Times.Once);
            // Add additional assertions as needed to verify that the error message was displayed
        }
    }
}
