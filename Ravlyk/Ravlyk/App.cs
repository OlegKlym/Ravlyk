using Acr.UserDialogs;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using Ravlyk.Views;

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
                .PerRequest<InfoViewModel>()
                .PerRequest<ISQLiteService>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<OrderService>()
                .Singleton<WebService>()
                .Singleton<DatabaseService>();


            Initialize();
           
            DisplayRootView<MainView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}