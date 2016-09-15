using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.ObjectModel;

namespace Ravlyk.ViewModels
{
    public class ShopViewModel : Screen
    {
        public string ShopId { get; set; }
       


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

        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public ShopViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
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

        protected override void OnActivate()
        {
            base.OnActivate();
            Shop = _dataService.LoadShopModelById(ShopId);

        }
    }
}
