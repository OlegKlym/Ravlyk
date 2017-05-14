using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Helpers;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using Ravlyk.Views;
using System;
using Xamarin.Forms;

namespace Ravlyk
{

    public class App : FormsApplication
    {
        public static bool AppStart = false; 
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
                .PerRequest<CheckoutViewModel>()
                .PerRequest<InfoViewModel>()
                .PerRequest<FavouriteViewModel>()
                .PerRequest<Helper>()
                .PerRequest<ISQLiteService>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<OrderService>()
                .Singleton<WebService>()
                .Singleton<Settings>()
                .Singleton<DatabaseService>();


            AppStart = true;
            Initialize();           
            DisplayRootView<MainView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }

       


    }
}