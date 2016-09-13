using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShopViewModel> Shops { get; set; }

        ShopViewModel selectedShop;
        public ICommand ClickBasketCommand { protected set; get; }
        public INavigation Navigation { get; set; }  
        private DataService _dataService = new DataService();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            ClickBasketCommand = new Command(ClickedBasket);
            LoadData();
        }

        public async Task LoadData()
        {

            Shops = new ObservableCollection<ShopViewModel>();

            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=63_147"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=72_81"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=98_99"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=59_62"));

            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=88_138"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=76_77"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=96_97"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=92_105"));

            Shops.Add(await _dataService.LoadShopAsync(" http://ravlyk.club/index.php?route=product/category&path=90_91"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=135_136"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=94_95"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=130_134"));

            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=155_157"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category&path=163_169"));
            Shops.Add(await _dataService.LoadShopAsync("http://ravlyk.club/index.php?route=product/category"));
        }

        public ShopViewModel SelectedShop
        {
            get { return selectedShop; }
            set
            {
                if (selectedShop != value)
                {
                    ShopViewModel tempShop = value;
                    selectedShop = null;                             
                    if (tempShop.Categories.Count == 1)
                    {
                       
                        Navigation.PushAsync(new CategoryView(tempShop.Categories[0]));
                    }

                    else
                    {
                       
                       Navigation.PushAsync(new ShopView(tempShop));
                    }
                       

                }
            }
        }

        public void ClickedBasket()
        {
            try
            {
                Navigation.PushAsync(new OrderView());
            }
            catch
            {

            }
           
        }


    }

}

