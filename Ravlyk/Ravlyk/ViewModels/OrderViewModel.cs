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

   

        public Command <OrderModel>StepperDecCommand { set; get; }
        public Command <OrderModel>StepperIncCommand { set; get; }
        public Command <OrderModel>DeleteOrderCommand { set; get; }


        private readonly DatabaseService _databaseService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        public OrderViewModel()
        {
            
        }

        public OrderViewModel(DatabaseService databaseService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            ClearOrdersCommand = new Command(ClearOrders);
            ConfirmCommand = new Command(Confirm);
            StepperDecCommand = new Command<OrderModel>(StepperDec);
            StepperIncCommand = new Command<OrderModel>(StepperInc);
            DeleteOrderCommand = new Command<OrderModel>(DeleteOrder);
            NavigationPage.SetHasBackButton(new MainView(), false);
        }

        public void ClearOrders()
        {
            IoC.Get<OrderService>().ClearOrders();
            _navigationService.GoBackToRootAsync();
        }

        public void Confirm()
        {
            if (IoC.Get<OrderService>().GetTotalPrice() > 69)
                _navigationService.For<FormViewModel>().Navigate();
            else
                Text = "Мінімальна сума замовлення 69грн";
        }

        public void StepperInc(OrderModel orderObject)
        {
            IoC.Get<OrderService>().StepperInc(orderObject);
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
            TotalPrice = IoC.Get<OrderService>().GetTotalPrice();
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());

        }

        public void StepperDec(OrderModel orderObject)
        {
            IoC.Get<OrderService>().StepperDec(orderObject);
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
            TotalPrice = IoC.Get<OrderService>().GetTotalPrice();
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
        }

        public void DeleteOrder(OrderModel orderObject)
        {
            if (orderObject != null)
            {
                IoC.Get<OrderService>().DeleteOrder(orderObject);
            }

            if (IoC.Get<OrderService>().GetOrders().Count == 0)
            {
                IoC.Get<INavigationService>().GoBackToRootAsync();

            }
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
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
