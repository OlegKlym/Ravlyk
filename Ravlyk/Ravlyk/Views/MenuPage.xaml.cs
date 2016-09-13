using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(string name)
        {
            Title = name;
            InitializeComponent();
        }
    }
}
