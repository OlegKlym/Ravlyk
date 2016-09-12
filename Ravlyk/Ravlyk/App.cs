﻿using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Ravlyk
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainView());
            //Xamarin.Forms.DependencyService.Register<DataService.Service>();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}