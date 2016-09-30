using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class FavouriteViewModel : Screen
    {
        public int DishId { get; set; }
        private ObservableCollection<DishModel> _favors;
        public ObservableCollection<DishModel> Favors
        {
            get { return _favors; }
            set
            {
                _favors = value;
                NotifyOfPropertyChange(() => Favors);
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
                if (_basketTitle == null)
                    return "До кошика";
                return "Додано";
            }
            set
            {
                _basketTitle = value;
                NotifyOfPropertyChange(() => BasketTitle);
            }
        }

        public ICommand ClickBasketCommand { set; get; }
        public Command<DishModel> AddDishCommand { set; get; }
        public Command<DishModel> RemoveFavorCommand { set; get; }
        DatabaseService _database;

        private readonly INavigationService _navigationService;
     
        public FavouriteViewModel (INavigationService navigationService )
        {
            _database = new DatabaseService();
            _navigationService = navigationService;
            ClickBasketCommand = new Command(ClickBasket);
            RemoveFavorCommand = new Command<DishModel>(RemoveFavor);
            AddDishCommand = new Command<DishModel>(AddDish);
        }

        protected void AddDish(DishModel dishObject)
        {
            IoC.Get<OrderService>().AddDish(dishObject);
            if (IoC.Get<OrderService>().GetOrders().Count == 0)
                Basket = "basket.png";
            else
                Basket = "plus.png";
            BasketTitle = "Додано";
        }

        public void RemoveFavor(DishModel dishObject)
        {
            Favors.Remove(dishObject);
            _database.SetFavor(dishObject.Id);
            if (_database.GetFavor().Count == 0)
            {
                IoC.Get<INavigationService>().GoBackToRootAsync();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Favors = _database.GetFavor();
        }

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }
    }
}
