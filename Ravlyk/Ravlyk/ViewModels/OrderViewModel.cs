using Ravlyk.Models;
using Ravlyk.Services;
using Ravlyk.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand ConfirmCommand { protected set; get; }
        public ICommand StepperDecCommand { protected set; get; }
        public ICommand StepperIncCommand { protected set; get; }
        public ICommand DeleteOrderCommand { protected set; get; }
        public ICommand ClearOrdersCommand { protected set; get; }
        private OrderModel order;
        public OrderItemModel OrderItem { get; set; }
        public ObservableCollection<OrderModel> Orders { get; set; }


        public OrderViewModel()
        {
            Order = new OrderModel();
            Orders = new ObservableCollection<OrderModel>();
            StepperDecCommand = new Command(StepperDec);
            StepperIncCommand = new Command(StepperInc);
            DeleteOrderCommand = new Command(DeleteOrder);
            ConfirmCommand = new Command(Confirm);
            ClearOrdersCommand = new Command(ClearOrders);
        }


        public ObservableCollection<OrderItemModel> OrderItems
        {
            get { return Order.OrderItems; }
            set { Order.OrderItems = value; }
        }

        public int TotalPrice
        {
            get { return OrderService.Instance.GetTotalPrice(); }
            set {  TotalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
           
        
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public OrderModel Order
        {
            get { return order; }
            set { order = value; }
        }

        public DishModel Dish
        {
            get { return OrderItem.Dish; }
            set { OrderItem.Dish = value; }
        }

        public int Count
        {
            get { return OrderItem.Count; }
            set { OrderItem.Count = value; }
        }

     

        private void StepperDec(object orderObject)
        {
            OrderItemModel orderItem = orderObject as OrderItemModel;
            OrderService.Instance.StepperDec(orderItem);
            OrderService.Instance.SetTotalPrice(orderItem.Dish.Price, false);
            TotalPrice = OrderService.Instance.GetTotalPrice();

        }

        private void StepperInc(object orderObject)
        {
            OrderItemModel orderItem = orderObject as OrderItemModel;
            OrderService.Instance.StepperInc(orderItem);

            OrderService.Instance.SetTotalPrice(orderItem.Dish.Price,true);
            TotalPrice = OrderService.Instance.GetTotalPrice();
        }


        public void DeleteOrder(object orderObject)
        {
            OrderItemModel orderItem = orderObject as OrderItemModel;
            if (orderItem != null)
            {
                OrderService.Instance.DeleteDish(orderItem);
            }
            Navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
            if (OrderService.Instance.GetOrders().Count == 0)
            {
                App.MasterDetailPage.Detail = new NavigationPage(new MainView());
            }
               
           



        }

        public void ClearOrders()
        {
            OrderService.Instance.Clear();
            App.MasterDetailPage.Detail = new NavigationPage(new MainView());
        }

        public void Confirm()
        {
            if (OrderService.Instance.GetTotalPrice() > 69)
            {
                Navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
                Navigation.PushModalAsync(new FormView(OrderService.Instance.GetOrders()));
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

      
    }
}
