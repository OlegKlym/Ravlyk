using Acr.UserDialogs;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Plugin.Connectivity;
using Ravlyk.Entities;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.Generic;
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
        public ICommand RefreshCommand { set; get; }
        public ICommand GetFavorCommand { set; get; }
        public DatabaseService _database;
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
                if(_database.GetCategoriesFromBD(value.Id).Count == 1)
                    IoC.Get<INavigationService>().For<CategoryViewModel>().WithParam(x => x.CategoryId, _database.GetCategoriesFromBD(value.Id)[0].Id ).WithParam(x => x.ShopId, value.Id).Navigate();
                else
                    IoC.Get<INavigationService>().For<ShopViewModel>().WithParam(x => x.ShopId, value.Id).Navigate();
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

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }

            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    NotifyOfPropertyChange(() => IsLoading);
                }
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }

            private set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    
                    NotifyOfPropertyChange(() => IsRefreshing);
                }
            }
        }

        public MainViewModel()
        {
            _database = new DatabaseService();
            Shops = new List<ShopModel>();
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);
            ClickInfoCommand = new Command(ClickInfo);
            RefreshCommand = new Command(Refreshing);

        }

        protected void ClickBasket()
        {
            if (IoC.Get<OrderService>().GetOrders().Count != 0)
                IoC.Get<INavigationService>().For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected void GetFavor()
        {
            if (_database.GetFavor().Count != 0)
            {
                IoC.Get<INavigationService>().For<FavouriteViewModel>().Navigate();
            }

        }

        protected void ClickInfo()
        {
            IoC.Get<INavigationService>().For<InfoViewModel>().Navigate();
        }

        protected void Refreshing()
        {
            _database.Database.DropTable<ShopEntity>();
            _database.Database.DropTable<CategoryEntity>();
            _database.Database.DropTable<DishEntity>();
            IsRefreshing = true;
            _database.Database.CreateTable<ShopEntity>();
            _database.Database.CreateTable<CategoryEntity>();
            _database.Database.CreateTable<DishEntity>();
            LoadShopsAsync();

        }


        protected override async void OnActivate()
        {
            base.OnActivate();

            if (CrossConnectivity.Current.IsConnected)
            {
               
                try
                {
                    _database.GetShops();
                    Shops = _database.GetShopsFromBD();
                }
                catch
                {
                    IsLoading = true;
                    _database.Database.CreateTable<ShopEntity>();
                    _database.Database.CreateTable<CategoryEntity>();
                    _database.Database.CreateTable<DishEntity>();
                    LoadShopsAsync();
                }
            }
            else
            {
                if (_database.GetShops().Count == 0)
                {
                    UserDialogs.Instance.Alert("Відсутнє з'єднання з інтернетом!");
                }

                else
                {
                    Shops = _database.GetShopsFromBD();
                }
            }
        }


        public async Task LoadShopsAsync()
        {
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=63_147", 1));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=72_81", 2));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=98_99", 3));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=59_62", 4));

            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=88_138", 5));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=76_77", 6));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=96_97", 7));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=92_105", 8));

            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync(" http://ravlyk.club/index.php?route=product/category&path=90_91", 9));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=135_136", 10));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=94_95", 11));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=130_134", 12));

            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=155_157", 13));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=163_169", 14));
            _database.InsertShop(await IoC.Get<WebService>().LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=161_162", 15));

            Shops = _database.GetShopsFromBD();
            IsLoading = false;
            IsRefreshing = false;
        }


    }
}

