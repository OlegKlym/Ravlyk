using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class ShopView : ContentPage
    {
        public ShopViewModel ViewModel { get; private set; }
        public ShopView(ShopViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            ViewModel.Navigation = Navigation;
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = ViewModel;

        }

    }
}
