using TravelBuddyApp.Interfaces;

namespace TravelBuddyApp.Services
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Location?> GetLastKnownLocationAsync()
        {
            return await Geolocation.GetLastKnownLocationAsync();
        }
    }
}