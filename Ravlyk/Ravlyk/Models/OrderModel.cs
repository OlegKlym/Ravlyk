using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Events;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.Models
{
    public class OrderModel : PropertyChangedBase
    {
        public DishModel Dish { get; set; }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(() => Count);
            }
        }

       
    }
}