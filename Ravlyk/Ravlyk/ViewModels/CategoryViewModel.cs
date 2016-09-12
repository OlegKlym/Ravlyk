using Ravlyk.Models;
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
            Navigation.PushAsync(new OrderView());

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
