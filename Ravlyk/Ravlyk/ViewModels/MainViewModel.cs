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
using System.Collections.Generic;
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
        public DatabaseService Database { set; get; }

        private List<ShopModel> _shops;
        public List<ShopModel> Shops
        {
            get { return _shops; }
            set
            {
                _shops = value;
                NotifyOfPropertyChange(() => Shops);
            }
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

        public MainViewModel()
        {
            Database = new DatabaseService();
            Shops = new List<ShopModel>();
            ClickBasketCommand = new Command(ClickBasket);
            ClickInfoCommand = new Command(ClickInfo);
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
                Database.ClearDB();
                Database.Database.DropTable<ShopEntity>();
                Database.Database.CreateTable<ShopEntity>();
                if (Database.GetShops().Count == 0)
                {
                    LoadShopsAsync();
                   
                }
                else
                {
                    Shops = Database.GetShopsFromBD();
                }

            }
            else
            {
                if (Database.GetShops().Count == 0)
                {
                    Internet = "Відсутнє з'єднання з інтернетом!";
                }
                else
                {
                    Shops = Database.GetShopsFromBD();
                }
            }
        }

        public async Task LoadShopsAsync()
        {
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=63_147", 1));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=72_81", 2));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=98_99", 3));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=59_62", 4));

            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=88_138", 5));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=76_77", 6));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=96_97", 7));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=92_105", 8));

            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync(" http://ravlyk.club/index.php?route=product/category&path=90_91", 9));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=135_136", 10));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=94_95", 11));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=130_134", 12));

            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=155_157", 13));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=163_169", 14));
            Database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=161_162", 15));
           
            Shops = Database.GetShopsFromBD();
           
        }

      
    }
}

