﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Ravlyk.Views
{
    public partial class FavouriteView : ContentPage
    {
        public FavouriteView()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
