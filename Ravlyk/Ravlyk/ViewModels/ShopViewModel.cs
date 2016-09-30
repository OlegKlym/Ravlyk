using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class ShopViewModel : Screen
    {
        public int ShopId { get; set; }
        public ICommand ClickBasketCommand { set; get; }
        public ICommand GetFavorCommand { set; get; }
        public DatabaseService _database;

        private ShopModel _shop;
        public ShopModel Shop
        {
            get
            {
                return _shop;
            }

            set
            {
                _shop = value;
                NotifyOfPropertyChange(() => Shop);
            }
        }

        private readonly WebService _webservice;
        private readonly INavigationService _navigationService;

        public ShopViewModel(WebService webservice, INavigationService navigationService)
        {
            _webservice = webservice;
            _navigationService = navigationService;
            _database = new DatabaseService();
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);
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

        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (value == null)
                    return;
                _navigationService.For<CategoryViewModel>().WithParam(x => x.CategoryId, value.Id).WithParam(x => x.ShopId, ShopId).Navigate();           
            }
        }

        protected void GetFavor()
        {
            if (_database.GetFavor().Count != 0)
            {
                _navigationService.For<FavouriteViewModel>().Navigate();
            }

        }

        protected void ClickBasket()
        {
            if (IoC.Get<OrderService>().GetOrders().Count != 0)
                IoC.Get<INavigationService>().For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Shop = new ShopModel()
            {
                Title = _database.GetTitle(ShopId, 0),
                Categories = _database.GetCategoriesFromBD(ShopId)
            };                         
        }        
    }
}
