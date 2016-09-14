using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

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

        protected override void OnActivate()
        {
            base.OnActivate();
            Shop = _dataService.LoadShopModelById(ShopId);
            
        }
    }
}
