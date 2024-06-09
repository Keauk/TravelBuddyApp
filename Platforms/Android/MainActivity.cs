using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Reflection;
using System.Text.Json;

namespace TravelBuddyApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var config = LoadConfig();
            if (config.TryGetValue("GoogleMapsApiKey", out var googleMapsApiKey))
            {
                SetApiKey(googleMapsApiKey);
            }
        }

        private Dictionary<string, string> LoadConfig()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "TravelBuddyApp.Resources.Raw.config.json";

            System.Diagnostics.Debug.WriteLine($"Trying to load resource: {resourceName}");

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                System.Diagnostics.Debug.WriteLine("Stream is null. Resource not found.");
                throw new FileNotFoundException("config.json embedded resource not found.");
            }

            using StreamReader reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            System.Diagnostics.Debug.WriteLine("config.json content:");
            System.Diagnostics.Debug.WriteLine(json);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        private void SetApiKey(string apiKey)
        {
            try
            {
                var ai = PackageManager.GetApplicationInfo(PackageName, PackageInfoFlags.MetaData);
                var bundle = ai.MetaData;
                bundle.PutString("com.google.android.geo.API_KEY", apiKey);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting API key: {ex.Message}");
            }
        }
    }
}
