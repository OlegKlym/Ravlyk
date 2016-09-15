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

        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public DishViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            AddDishCommand = new Command(AddDish);
            ClickBasketCommand = new Command(ClickBasket);

        }

        protected void AddDish()
        {
            OrderService.Instance.AddDish(Dish);
            OrderService.Instance.SetTotalPrice();
        }

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().Navigate();
        }


        protected override void OnActivate()
        {
            base.OnActivate();           
            Dish = _dataService.LoadDishModelById(ShopId, CategoryId, DishId);
        }
    }
}
