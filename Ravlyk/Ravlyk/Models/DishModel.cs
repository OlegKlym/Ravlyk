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

        public ICommand AddDishCommand { set; get; }
        public ICommand DeleteDishCommand { set; get; }
        public string _basket;
        public string Basket
        {
            get
            {
                if (IoC.Get<OrderService>().GetOrders().Count == 0)
                    return "basket.png";
                else
                    return "plus.png";
            }
            set
            {
                _basket = value;
                NotifyOfPropertyChange(() => Basket);
            }
        }

        public string _basketTitle;
        public string BasketTitle
        {
            get
            {
                if (_basketTitle == null)
                    return "До кошика";
                return "Додано";
            }
            set
            {
                _basketTitle = value;
                NotifyOfPropertyChange(() => BasketTitle);
            }
        }

        DatabaseService _database;

        public DishModel()
        {
            _database = new DatabaseService();
            AddDishCommand = new Command(AddDish);
            DeleteDishCommand = new Command(DeleteDish);
        }

        protected void AddDish(object dishObject)
        {
            DishModel dishItem = dishObject as DishModel;
            IoC.Get<OrderService>().AddDish(dishItem);
            if (IoC.Get<OrderService>().GetOrders().Count == 0)
                Basket = "basket.png";
            else
                Basket = "plus.png";
            BasketTitle = "Додано";

        }

        protected void DeleteDish(object dishObject)
        {
            DishModel dishItem = dishObject as DishModel;
            _database.SetFavor(dishItem.Id);
            IoC.Get<FavouriteViewModel>().RemoveFavor(dishItem);
            if (_database.GetFavor().Count == 0)
            {
                IoC.Get<INavigationService>().GoBackToRootAsync();
            }
           
        }
    }
}
