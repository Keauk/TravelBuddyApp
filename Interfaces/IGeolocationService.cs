namespace TravelBuddyApp.Interfaces
{
    public interface IGeolocationService
    {
        Task<Location?> GetLastKnownLocationAsync();
    }
}
