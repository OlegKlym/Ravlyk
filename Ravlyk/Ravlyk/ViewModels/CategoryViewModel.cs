using Acr.UserDialogs;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class CategoryViewModel : Screen
    {
        public int CategoryId { get; set ; }
        public int ShopId { get; set; }

        public ICommand ClickBasketCommand { set; get; }
        public ICommand GetFavorCommand { set; get; }

        public CategoryModel Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
                NotifyOfPropertyChange(() => Category);
            }
        }

        private DishModel _selectedDish;
        public DishModel SelectedDish
        {
            get
            {
                return _selectedDish;
            }

            set
            {
                if (value == null)
                    return;
                _navigationService.For<DishViewModel>().WithParam(x => x.DishId, value.Id).
                    WithParam(x => x.CategoryId, CategoryId).WithParam(x => x.ShopId, ShopId).Navigate();

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

        private DatabaseService _database;
        private CategoryModel _category;
        private readonly WebService _webService;
        private readonly INavigationService _navigationService;

        public CategoryViewModel(WebService webService, INavigationService navigationService)
        {
            _webService = webService;
            _navigationService = navigationService;
            _database = new DatabaseService();
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);
        }

        protected async void ClickBasket()
        {
            if (IoC.Get<OrderService>().GetOrders().Count != 0)
            {
                IoC.Get<INavigationService>().For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Корзина порожня!");
            }

        }

        protected async void GetFavor()
        {
            if (_database.GetFavor().Count != 0)
            {
                IoC.Get<INavigationService>().For<FavouriteViewModel>().Navigate();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Список улюблених страв порожній!");
            }
        }

        protected override void OnActivate()
        {
           base.OnActivate();
            Category = new CategoryModel()
            {
               Title = _database.GetTitle(ShopId,CategoryId),
                Dishes = _database.GetDishesFromBD(ShopId,CategoryId)
            };

        }
       
    }
}
