using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Events;
using Ravlyk.Services;
using Ravlyk.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.Models
{
    public class OrderModel : PropertyChangedBase
    {
        public DishModel Dish { get; set; }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyOfPropertyChange(() => Count);
            }
        }

        public ICommand StepperDecCommand { set; get; }
        public ICommand StepperIncCommand { set; get; }
        public ICommand DeleteOrderCommand { set; get; }

        public OrderModel()
        {
            StepperDecCommand = new Command(StepperDec);
            StepperIncCommand = new Command(StepperInc);
            DeleteOrderCommand = new Command(DeleteOrder);
        }

        public void StepperInc(object orderObject)
        {
            OrderModel orderItem = orderObject as OrderModel;
            IoC.Get<OrderService>().StepperInc(orderItem);

            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
        }

        public void StepperDec(object orderObject)
        {
            OrderModel orderItem = orderObject as OrderModel;
            IoC.Get<OrderService>().StepperDec(orderItem);
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());

        }

        public void DeleteOrder(object orderObject)
        {
            OrderModel orderItem = orderObject as OrderModel;
            if (orderItem != null)
            {
                IoC.Get<OrderService>().DeleteOrder(orderItem);
            }

            if (IoC.Get<OrderService>().GetOrders().Count == 0)
            {
                IoC.Get<INavigationService>().GoBackToRootAsync();
               
            }
            IoC.Get<IEventAggregator>().PublishOnUIThread(new ProductUpdate());
        }



    }
}