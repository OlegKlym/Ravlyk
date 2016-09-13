using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class DishView : ContentPage
    {
        public DishView(DishViewModel vm)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = vm;
            vm.Navigation = Navigation;
        }
    }
}
