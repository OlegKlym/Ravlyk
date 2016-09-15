using Ravlyk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ravlyk.Services
{
    public class OrderService
    {
        private ObservableCollection<OrderModel> _orders = new ObservableCollection<OrderModel>();
        private int _totalPrice;
        private static OrderService _instance;
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


        public ObservableCollection<OrderModel> GetOrders()
        {
            return _orders;
        }

        public void AddDish(DishModel dish)
        {
            bool isContains = false;       
            foreach (var item in _orders)
                if (item.Dish == dish)
                {
                    item.Count++;
                    isContains = true;
                    break;
                }

            if (!isContains)
                _orders.Add(new OrderModel() { Dish = dish, Count = 1});
        }

        public void ClearOrders()
        {
            _orders.Clear();
        }

        public void StepperInc(OrderModel order)
        {
            foreach (var item in _orders)
                if (item.Dish == order.Dish)
                {
                    item.Count++;
                    break;
                }
        }

        public void StepperDec(OrderModel order)
        {
            foreach (var item in _orders)
                if (item.Dish == order.Dish && item.Count > 1)
                {
                    item.Count--;
                    break;
                }
        }

        public void DeleteOrder(OrderModel order)
        {
            _orders.Remove(order);
        }

        public void SetTotalPrice()
        {
            _totalPrice = 0;
            foreach (var item in _orders)
            {
                string text = Regex.Replace(item.Dish.Price, "[^0-9]", "", RegexOptions.Singleline);
                _totalPrice += (int.Parse(text) * item.Count);
            }
        }

        public int GetTotalPrice()
        {
            return _totalPrice;
        }
    }
}
