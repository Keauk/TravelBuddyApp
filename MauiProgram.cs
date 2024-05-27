﻿using CommunityToolkit.Maui;
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
