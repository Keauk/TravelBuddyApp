using CommunityToolkit.Maui;
using TravelBuddyApp.Interfaces;


#if ANDROID
using TravelBuddyApp.Platforms.Android;
#endif

using TravelBuddyApp.Services;
using TravelBuddyApp.ViewModels;
using TravelBuddyApp.Views;

namespace TravelBuddyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            #if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                handlers.AddHandler(typeof(Editor), typeof(EditorHandler));
            });
            #endif

            // Register services
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IGeolocationService, GeolocationService>();

            // Register ViewModels
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LandingPageViewModel>();

            // Register Views
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LandingPage>();

            return builder.Build();
        }
    }
}
