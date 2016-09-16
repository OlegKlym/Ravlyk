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
        public string DishId { get; set; }
        public string CategoryId { get; set; }
        public string ShopId { get; set; }
        public ICommand AddDishCommand { set; get; }
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


        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public DishViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            AddDishCommand = new Command(AddDish);
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

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();           
            Dish = _dataService.LoadDishModelById(ShopId, CategoryId, DishId);
        }
    }
}
