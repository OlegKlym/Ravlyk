using Ravlyk.Models;
using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class FormView : ContentPage
    {
        public FormView(ObservableCollection<OrderItemModel> list)
        {
            InitializeComponent();
            BindingContext = new FormViewModel() { Navigation = this.Navigation };
           
        }
    }
}
