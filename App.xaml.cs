﻿using System.Net;
using TravelBuddyApp.Views;

namespace TravelBuddyApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
