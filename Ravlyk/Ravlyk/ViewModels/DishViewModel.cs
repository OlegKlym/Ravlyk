using Acr.UserDialogs;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class DishViewModel : Screen
    {
        public int DishId { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public ICommand AddDishCommand { set; get; }
        public ICommand FavorCommand { set; get; }
        public ICommand GetFavorCommand { set; get; }
        public ICommand ClickBasketCommand { set; get; }

        private DishModel _dish;
        public DishModel Dish
        {
            get
            {
                return _dish;
            }

            set
            {
                _dish = value;
                NotifyOfPropertyChange(() => Dish);
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

        public string _basketTitle;
        public string BasketTitle
        {
            get
            {
                var orders = IoC.Get<OrderService>().GetOrders();
                foreach (var item in orders)
                {
                    if (item.Dish.Id == DishId)
                        return "Додано";
                }
                return "В кошик";
            }
            set
            {
                _basketTitle = value;
                NotifyOfPropertyChange(() => BasketTitle);
            }
        }

        private string _favourite;
        public string Favourite
        {
            get
            {
                if (_database.IsFavor(DishId))
                    return "Вилучити з улюблених";
                else
                    return "Додати до улюблених";
            }
            set
            {
                _favourite = value;
                NotifyOfPropertyChange(() => Favourite);
            }
        }

        private DatabaseService _database;
        private readonly WebService _webService;
        private readonly INavigationService _navigationService;

        public DishViewModel(WebService webService, INavigationService navigationService)
        {
            _webService = webService;
            _navigationService = navigationService;
            _database = new DatabaseService();
            AddDishCommand = new Command(AddDish);
            FavorCommand = new Command(Favor);
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);
        }

        protected void AddDish()
        {
            IoC.Get<OrderService>().AddDish(Dish);
            if (IoC.Get<OrderService>().GetOrders().Count == 0)
                Basket = "basket.png";
            else
                Basket = "plus.png";
            BasketTitle = "Додано";
        }

        protected void Favor()
        {
            _database.SetFavor(DishId);
            if (_database.IsFavor(DishId))
                Favourite = "Вилучити з улюблених";
            else
                Favourite = "Додати до улюблених";
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
            Dish = _database.GetDish(DishId);
        }
    }
}
