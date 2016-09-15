using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Ravlyk.Models;
using Ravlyk.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class OrderViewModel : Screen
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
            get { return OrderService.Instance.GetTotalPrice(); }
            set
            {
                OrderService.Instance.SetTotalPrice();
                NotifyOfPropertyChange(() => TotalPrice);
            }
        }


        private readonly DataService _dataService;
        private readonly INavigationService _navigationService;

        public OrderViewModel()
        {

        }
        public OrderViewModel(DataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            ClearOrdersCommand = new Command(ClearOrders);
            ConfirmCommand = new Command(Confirm);
           
        }

        public void ClearOrders()
        {
            OrderService.Instance.ClearOrders();
            _navigationService.For<MainViewModel>().Navigate();
        }

        public void Confirm()
        {
            if(IoC.Get<OrderService>().GetTotalPrice() > 69)
                _navigationService.For<FormViewModel>().Navigate();
        }

       

        protected override void OnActivate()
        {
            base.OnActivate();
            Orders = OrderService.Instance.GetOrders();
        }





    }
}
