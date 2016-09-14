using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class DishViewModel : INotifyPropertyChanged
    {
        private string basket;
        public DishModel Dish { get; private set; }
        public ICommand AddDishCommand { protected set; get; }
        public ICommand ClickBasketCommand { protected set; get; }
        public INavigation Navigation { get; set; }
        public OrderModel order = new OrderModel();
        DishViewModel selectedDish;

        public event PropertyChangedEventHandler PropertyChanged;

        public DishViewModel()
        {
            Dish = new DishModel();
            this.AddDishCommand = new Command(AddDish);
            ClickBasketCommand = new Command(ClickedBasket);
        }

        public string Title
        {
            get { return Dish.Title; }
            set { Dish.Title = value; }
        }

        public string Price
        {
            get { return Dish.Price; }
            set { Dish.Price = value; }
        }

        public string Description
        {
            get { return Dish.Description; }
            set { Dish.Description = value; }
        }

        public string ImagePath
        {
            get { return Dish.ImagePath; }
            set { Dish.ImagePath = value; }
        }

        public DishViewModel SelectedDish
        {
            get { return selectedDish; }
            set
            {
                if (selectedDish != value)
                {
                    DishViewModel tempDish = value;
                    selectedDish = null;
                    Navigation.PushAsync(new DishView(tempDish));

                }
            }
        }

      
        public string Basket
        {
            get
            {
                if (OrderService.Instance.GetOrders() == null)
                {
                    return "basket.png";
                }
                else
                {
                    if (OrderService.Instance.GetOrders().Count == 0)
                        return "basket.png";
                    else
                        return "plus.png";
                }
            }
            set
            {
                basket = value;
                OnPropertyChanged("Basket");

            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public void ClickedBasket()
        {
            try { Navigation.PushAsync(new OrderView()); }
            catch { }
        }

        public void AddDish()
        {
            OrderService.Instance.AddDish(Dish);
            if (OrderService.Instance.GetOrders() == null)
            {
                Basket = "basket.png";
            }
            else
            {
                if (OrderService.Instance.GetOrders().Count == 0)
                    Basket = "basket.png";
                else
                    Basket = "plus.png";
            }
        }
    }
}
