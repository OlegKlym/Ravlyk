using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class CategoryView : ContentPage
    {
        public CategoryView(CategoryViewModel vm)
        {
            InitializeComponent();
            vm.Navigation = Navigation;
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = vm;
        }

    }
}
