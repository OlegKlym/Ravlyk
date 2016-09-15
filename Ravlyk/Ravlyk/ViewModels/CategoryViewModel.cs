using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;

namespace Ravlyk.ViewModels
{
    public class CategoryViewModel : Screen
    {
        public string CategoryId { get; set ; }
        public string ShopId { get; set; }

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


        protected override void OnActivate()
        {
            base.OnActivate();
            
            Category = _dataService.LoadCategoryModelById(ShopId, CategoryId);

        }
    }
}
