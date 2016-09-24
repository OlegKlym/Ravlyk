using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Events;
using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace Ravlyk.ViewModels
{
    public class OrderViewModel : Screen, IHandle<ProductUpdate>
    {
        public ICommand ClearOrdersCommand { set; get; }
        public ICommand ConfirmCommand { set; get; }


      
        private ObservableCollection<OrderModel> _orders;
        public ObservableCollection<OrderModel> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                NotifyOfPropertyChange(() => Orders);
            }
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get { return IoC.Get<OrderService>().GetTotalPrice();}
            set
            {
                _totalPrice = value ;
                NotifyOfPropertyChange(() => TotalPrice);
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        private readonly WebService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        public OrderViewModel()
        {
           
        }
        public OrderViewModel(WebService dataService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            ClearOrdersCommand = new Command(ClearOrders);
            ConfirmCommand = new Command(Confirm);
            NavigationPage.SetHasBackButton(new MainView(), false);
        }

        public void ClearOrders()
        {
            IoC.Get<OrderService>().ClearOrders();
            _navigationService.GoBackToRootAsync();
        }

        public void Confirm()
        {

            Text = "Дана функція поки  в розробці";
            if (IoC.Get<OrderService>().GetTotalPrice() > 69)
                _navigationService.For<FormViewModel>().Navigate();
        }
      
        protected override void OnActivate()
        {
          
            base.OnActivate();

            _eventAggregator.Subscribe(this);

            Orders = IoC.Get<OrderService>().GetOrders();
           
        }

        public void Handle(ProductUpdate message)
        {
            TotalPrice = _totalPrice;
        }
    }
}
