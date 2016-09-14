using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class CategoryViewModel
    {
        public INavigation Navigation { get; set; }
        public ICommand ClickBasketCommand { protected set; get; }
        public CategoryModel Category { get; private set; }

        DishViewModel selectedDish;

        public CategoryViewModel()
        {
            Category = new CategoryModel();
            ClickBasketCommand = new Command(ClickedBasket);
        }

        public string Title
        {
            get { return Category.Title; }
            set { Category.Title = value; }
        }

        public string ImagePath
        {
            get { return Category.ImagePath; }
            set { Category.ImagePath = value; }
        }

        public List<DishViewModel> Dishes
        {
            get { return Category.Dishes; }
            set { Category.Dishes = value; }
        }


        public void ClickedBasket()
        {
            try { Navigation.PushAsync(new OrderView()); }
            catch { }
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
                        return  "plus.png";
                }
            }
            set { Basket = value; }
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
    }
}
