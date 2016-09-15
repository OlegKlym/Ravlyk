﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class MainViewModel : Screen
    {
        private ObservableCollection<ShopModel> _shops;
        public ObservableCollection<ShopModel> Shops
        {
            get { return _shops; }
            set
            {
                _shops = value;
                NotifyOfPropertyChange(() => Shops);
            }
        }
        
        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public MainViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public ShopModel _selectedShop;
        public ShopModel SelectedShop
        {
            get
            {
                return _selectedShop;
            }

            set
            {
                if (value == null)
                    return;
                    if (value.Categories.Count == 1)
                    {

                    _navigationService.For<CategoryViewModel>().WithParam(x => x.CategoryId, value.Categories[0].Id).WithParam(x => x.ShopId, value.Id).Navigate();
                }

                    else
                    {
                        _navigationService.For<ShopViewModel>().WithParam(x => x.ShopId, value.Id).Navigate();
                    }
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();          
            // TODO: show progress indicator
            LoadDataAsync();
            // TODO: hide progress indicator
        }

        private async Task LoadDataAsync()
        {
            Shops = new ObservableCollection<ShopModel>();

            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=63_147", "1"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=72_81", "2"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=98_99", "3"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=59_62", "5"));
        
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=88_138", "6"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=76_77", "7"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=96_97", "8"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=92_105", "9"));

            Shops.Add(await _dataService.LoadShopAsync(" http://ravlyk.club/index.php?route=product/category&path=90_91", "10"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=135_136", "11"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=94_95", "12"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=130_134", "13"));

            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=155_157", "14"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=163_169", "15"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category", "16"));
        }
    }
}

