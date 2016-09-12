using Ravlyk.Services;
using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class OrderView : ContentPage
    {
        public OrderView()
        {
            InitializeComponent();
            BindingContext = new OrderViewModel() { OrderItems = OrderService.Instance.GetOrders() };
        }
    }
}
