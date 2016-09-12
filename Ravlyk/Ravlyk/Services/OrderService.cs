using Ravlyk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Services
{
    public class OrderService :INotifyPropertyChanged
    {

        public static OrderService _instance;

        public static OrderService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderService();
                }

                return _instance;
            }
        }

        private OrderModel _orderModel = new OrderModel();

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddDish(DishModel dish)
        {

            bool isContains = false;
            OrderItemModel newOrder = new OrderItemModel()
            {
                Dish = dish,
                Count = 1

            };

            if (_orderModel.OrderItems == null)
            {
                _orderModel.OrderItems = new ObservableCollection<OrderItemModel>();
            }

            foreach (var item in _orderModel.OrderItems)
                if (item.Dish == newOrder.Dish)
                {
                    item.Count++;
                    isContains = true;
                    break;
                }

            if (!isContains)
                _orderModel.OrderItems.Add(newOrder);
        }


        public void Clear()
        {
            _orderModel.OrderItems.Clear();
        }

        public void DeleteDish(OrderItemModel dish)
        {
            _orderModel.OrderItems.Remove(dish);
        }

        public ObservableCollection<OrderItemModel> GetOrders()
        {
            return _orderModel.OrderItems;
        }

        public void StepperInc(OrderItemModel order)
        {
            foreach (var item in _orderModel.OrderItems)
                if (item.Dish == order.Dish)
                {
                    item.Count++;
                    break;
                }

        }

        public void StepperDec(OrderItemModel order)
        {
            foreach (var item in _orderModel.OrderItems)
                if (item.Dish == order.Dish)
                {
                    if (item.Count > 1)
                        item.Count--;
                    break;
                }
        }
    }
}
