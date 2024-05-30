using CommunityToolkit.Maui;
using TravelBuddyApp.Platforms.Android;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                });

            // Register services
            builder.Services.AddSingleton<ApiService>();

            // Register ViewModels
            builder.Services.AddTransient<RegisterViewModel>();

            // Register Views
            builder.Services.AddTransient<RegisterPage>();

            return builder.Build();
        }
    }
}
