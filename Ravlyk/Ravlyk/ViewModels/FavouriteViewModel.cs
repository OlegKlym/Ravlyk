using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class FavouriteViewModel : Screen
    {
        public int DishId { get; set; }
        private ObservableCollection<DishModel> _favors;
        public ObservableCollection<DishModel> Favors
        {
            get { return _favors; }
            set
            {
                _favors = value;
                NotifyOfPropertyChange(() => Favors);
            }
        }
        public ICommand ClickBasketCommand { set; get; }
        public Command<DishModel> RemoveFavorCommand { set; get; }
        DatabaseService _database;




        private readonly INavigationService _navigationService;

      
        public FavouriteViewModel (INavigationService navigationService )
        {
            _database = new DatabaseService();
            _navigationService = navigationService;
            ClickBasketCommand = new Command(ClickBasket);
            RemoveFavorCommand = new Command<DishModel>(RemoveFavor);


        }

        public void RemoveFavor(DishModel dishObject)
        {
            Favors.Remove(dishObject);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Favors = _database.GetFavor();
        }


        protected void ClickBasket()
        {
            _navigationService.For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
        }
    }
}
