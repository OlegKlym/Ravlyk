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
    public class ShopViewModel
    {
        public ShopModel Shop { get; private set; }
        CategoryViewModel selectedCategory;
        public INavigation Navigation { get; set; }
        public ICommand ClickBasketCommand { protected set; get; }

        public ShopViewModel()
        {
            ClickBasketCommand = new Command(ClickedBasket);
            Shop = new ShopModel();
        }


        public string Title
        {
            get { return Shop.Title; }
            set { Shop.Title = value; }
        }

        public string Address
        {
            get { return Shop.Address; }
            set { Shop.Address = value; }
        }

        public string WorkTime
        {
            get { return Shop.WorkTime; }
            set
            {
                Shop.WorkTime = value;
            }
        }

        public string Type
        {
            get { return Shop.Type; }
            set
            {
                Shop.Type = value;
            }
        }

        public string Description
        {
            get { return Shop.Description; }
            set
            {
                Shop.Description = value;
            }
        }

        public string ImagePath
        {
            get { return Shop.ImagePath; }
            set
            {
                Shop.ImagePath = value;
            }
        }

        public List<CategoryViewModel> Categories
        {
            get { return Shop.Categories; }
            set
            {
                Shop.Categories = value;

            }
        }



        public void ClickedBasket()
        {
            Navigation.PushAsync(new OrderView());

        }

        public CategoryViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value)
                {
                    CategoryViewModel tempCategory = value;
                    selectedCategory = null;


                    Navigation.PushAsync(new CategoryView(tempCategory));

                }
            }
        }

    }
}
