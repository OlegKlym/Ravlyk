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
        public string CategoryId { get; set ; }
        public string ShopId { get; set; }
        public ICommand ClickBasketCommand { set; get; }

        private CategoryModel _category;
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

        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public CategoryViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            ClickBasketCommand = new Command(ClickBasket);
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
                if( IoC.Get<OrderService>().GetOrders().Count == 0)
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

        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            
            Category = _dataService.LoadCategoryModelById(ShopId, CategoryId);

        }
    }
}
