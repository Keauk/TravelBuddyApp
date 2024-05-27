using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using TravelBuddyApp.Services;
using TravelBuddyApp.ViewModels;

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
