using TravelBuddyApp.Models;

public interface IApiService
{
    Task<HttpResponseMessage> CreateTripAsync(TripInput tripInput);
    Task<HttpResponseMessage> CreateTripLogAsync(TripLogInput tripLogInput, int tripId);
    Task<string> UploadPhotoAsync(Stream photoStream, string fileName);
    Task<HttpResponseMessage> RegisterUserAsync(UserInput userInput);
    Task<HttpResponseMessage> LoginUserAsync(UserLogin userLogin);
    Task<IEnumerable<TripLogResponse>> GetCurrentLogsForTripAsync(int tripId);
    Task<IEnumerable<TripResponse>> GetMyTripsAsync();
    Task<IEnumerable<TripResponse>> GetAllTripsAsync();
    Task<UserResponse> GetCurrentUserAsync();
}
