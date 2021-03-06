﻿using Acr.UserDialogs;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class ShopViewModel : Screen
    {
        public int ShopId { get; set; }
        public ICommand ClickBasketCommand { set; get; }
        public ICommand GetFavorCommand { set; get; }
        public DatabaseService _database;


        private string _shopImage;
        public string ShopImage
        {
            get
            {
                return _shopImage;
            }

            set
            {
                _shopImage = value;
                NotifyOfPropertyChange(() => ShopImage);
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private List<CategoryModel> _categories;
        public List<CategoryModel> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        private readonly WebService _webservice;
        private readonly INavigationService _navigationService;

        public ShopViewModel(WebService webservice, INavigationService navigationService)
        {
            _webservice = webservice;
            _navigationService = navigationService;
            _database = new DatabaseService();
            GetFavorCommand = new Command(GetFavor);
            ClickBasketCommand = new Command(ClickBasket);
        }

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

        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (value == null)
                    return;
                _navigationService.For<CategoryViewModel>().WithParam(x => x.CategoryId, value.Id).WithParam(x => x.ShopId, ShopId).Navigate();           
            }
        }

        protected async void ClickBasket()
        {
            if (IoC.Get<OrderService>().GetOrders().Count != 0)
            {
                IoC.Get<INavigationService>().For<OrderViewModel>().WithParam(x => x.TotalPrice, IoC.Get<OrderService>().GetTotalPrice()).Navigate();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Корзина порожня!");
            }

        }

        protected async void GetFavor()
        {
            if (_database.GetFavor().Count != 0)
            {
                IoC.Get<INavigationService>().For<FavouriteViewModel>().Navigate();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Список улюблених страв порожній!");
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Categories = _database.GetCategoriesFromBD(ShopId);
            Title = _database.GetShop(ShopId).Title;
            ShopImage = _database.GetShop(ShopId).ImagePath;
        }        
    }
}
