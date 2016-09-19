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

        private readonly WebService _dataService;
        private readonly INavigationService _navigationService;

        public ShopViewModel(WebService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
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

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Shop = _dataService.LoadShopModelById(ShopId);

        }
    }
}
