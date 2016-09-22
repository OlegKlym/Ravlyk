using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand AddToFavorCommand { set; get; }
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

        private readonly WebService _webService;
        private readonly INavigationService _navigationService;

        public DishViewModel(WebService webService, INavigationService navigationService)
        {
            _webService = webService;
            _navigationService = navigationService;
            _database = new DatabaseService();
            AddDishCommand = new Command(AddDish);
            AddToFavorCommand = new Command(AddToFavor);
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);

        }

        public string _basket;

        protected void AddDish()
        {
            IoC.Get<OrderService>().AddDish(Dish);
            if (IoC.Get<OrderService>().GetOrders().Count == 0)
                Basket = "basket.png";
            else
                Basket = "plus.png";
        }

        protected void AddToFavor()
        {
            _database.SetFavor(DishId);
        }

        protected void GetFavor()
        {
            _database.GetFavor();
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }



        protected override void OnActivate()
        {
            base.OnActivate();
            Dish = _database.GetDish(DishId);
        }
    }
}
