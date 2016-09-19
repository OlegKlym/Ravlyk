using Acr.UserDialogs;
using Android.App;
using Android.Views;
using Android.Widget;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Plugin.Connectivity;
using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class MainViewModel : Screen
    {
        public bool IsConnected { get; }
        public ICommand ClickBasketCommand { set; get; }
        public ICommand ClickInfoCommand { set; get; }
       

        private ObservableCollection<ShopModel> _shops;
        public ObservableCollection<ShopModel> Shops
        {
            get { return _shops; }
            set
            {
                _shops = value;              
                NotifyOfPropertyChange(() => Shops);
            }
        }

        
        public MainViewModel()
        {
            ClickBasketCommand = new Command(ClickBasket);
            ClickInfoCommand = new Command(ClickInfo);
        }

        public ShopModel _selectedShop;
        public ShopModel SelectedShop
        {
            get
            {
                return _selectedShop;
            }

            set
            {
                if (value == null)
                    return;
                if (value.Categories.Count == 1)
                {                   
                   IoC.Get<INavigationService>().For<CategoryViewModel>().WithParam(x => x.CategoryId, value.Categories[0].Id).WithParam(x => x.ShopId, value.Id).Navigate();
                }

                else
                {
                    IoC.Get<INavigationService>().For<ShopViewModel>().WithParam(x => x.ShopId, value.Id).Navigate();
                }
            }
        }

        public string _basket;
        public string Basket
        {
            get
            {
                if (IoC.Get<OrderService>().GetOrders().Count == 0)
                    return "basket.png";
                else
                    return "plus.png";
            }
            set
            {
                _basket = value;
                NotifyOfPropertyChange(() => Basket);
            }
        }

        private string _internet;
        public string Internet
        {
            get { return _internet; }
            set
            {
                _internet = value;
                NotifyOfPropertyChange(() => Internet);
            }
        }

        protected void ClickBasket()
        {
            IoC.Get<INavigationService>().For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected void ClickInfo()
        {
            IoC.Get<INavigationService>().For<InfoViewModel>().Navigate();
        }



        protected override void OnActivate()
        {
            base.OnActivate();
            if (CrossConnectivity.Current.IsConnected)
            {
              
                    LoadDataAsync();                 
            }
            else
            {
                Internet = "Відсутнє з'єднання з інтернетом!";
            }


        }

        private async Task LoadDataAsync()
        {
            Shops = new ObservableCollection<ShopModel>();
            
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=63_147", 1));           
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=72_81", 2));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=98_99", 3));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=59_62", 4));

            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=88_138", 5));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=76_77", 6));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=96_97", 7));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=92_105", 8));

            Shops.Add(await IoC.Get<WebService>().LoadShopAsync(" http://ravlyk.club/index.php?route=product/category&path=90_91", 9));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=135_136", 10));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=94_95", 11));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=130_134", 12));

            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=155_157", 13));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=163_169", 14));
            Shops.Add(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category", 15));


        }
    }
}

