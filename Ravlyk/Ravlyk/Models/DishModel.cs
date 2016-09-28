using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.Models
{
    public class DishModel : PropertyChangedBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool Favourite { get; set; }    
    }
}
