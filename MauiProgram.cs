using CommunityToolkit.Maui;

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
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<LocationService>();

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
