using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Ravlyk
{

    public class App : FormsApplication
    {
        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            this.container = container;

            container
                .PerRequest<MainViewModel>()
                .PerRequest<ShopViewModel>()
                .PerRequest<CategoryViewModel>()
                .PerRequest<DishViewModel>()
                .PerRequest<OrderViewModel>()
                .PerRequest<FormViewModel>()
                .Singleton<OrderService>()
                .Singleton<DataService>();


            Initialize();

            DisplayRootView<MainView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }

    //public class App : Application
    //{
    //    public static MasterDetailPage MasterDetailPage;
    //    public App()
    //    {
    //        MasterDetailPage = new MasterDetailPage
    //        {
    //            Master = new MenuPage("Main"),
    //            Detail = new NavigationPage(new MainView()),
    //        };
    //        MainPage = MasterDetailPage;
    //        //Xamarin.Forms.DependencyService.Register<DataService.Service>();

    //    }

    //    protected override void OnStart()
    //    {
    //        // Handle when your app starts
    //    }

    //    protected override void OnSleep()
    //    {
    //        // Handle when your app sleeps
    //    }

    //    protected override void OnResume()
    //    {
    //        // Handle when your app resumes
    //    }
    //}
}


// https://github.com/Caliburn-Micro/Caliburn.Micro/tree/3.0.0/samples/Hello.Forms/Hello.Forms