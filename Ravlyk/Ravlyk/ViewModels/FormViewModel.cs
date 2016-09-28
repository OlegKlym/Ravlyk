using Caliburn.Micro;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ravlyk.Services;
using System.Windows.Input;
using Caliburn.Micro.Xamarin.Forms;
using System.Net.Http;

using Ravlyk.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Acr.UserDialogs;

namespace Ravlyk.ViewModels
{

    public class FormViewModel : Screen
    {
        public ICommand ConfirmCommand { set; get; }

        public CustomerModel _customer;
        public string UserName
        {
            get
            {
                return IoC.Get<Settings>().UserName;
            }
            set
            {

                if (IoC.Get<Settings>().UserName == value)
                    return;
                IoC.Get<Settings>().UserName = value;
                NotifyOfPropertyChange(() => UserName);
            }

        }

        public string Adress
        {
            get
            {
                return IoC.Get<Settings>().Adress;
            }
            set
            {
                if (IoC.Get<Settings>().Adress == value)
                    return;
                IoC.Get<Settings>().Adress = value;
                NotifyOfPropertyChange(() => Adress);
            }

        }

        public string Email
        {
            get
            {
                return IoC.Get<Settings>().Email;
            }
            set
            {
                if (IoC.Get<Settings>().Email == value)
                    return;
                IoC.Get<Settings>().Email = value;
                NotifyOfPropertyChange(() => Email);
            }

        }

        public string Phone
        {
            get
            {
                return IoC.Get<Settings>().Phone;
            }
            set
            {
                if (IoC.Get<Settings>().Phone == value)
                    return;
                IoC.Get<Settings>().Phone = value;
                NotifyOfPropertyChange(() => Phone);
            }

        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        private INavigationService _navigationService;

        public FormViewModel(INavigationService navigationService)
        {
            ConfirmCommand = new Command(Confirm);
            _navigationService = navigationService;
            _customer = new CustomerModel();
        }



        public void Confirm()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cookie", "PHPSESSID=" + Guid.NewGuid().ToString());

            var orders = IoC.Get<OrderService>().GetOrders();
            foreach (var order in orders)
            {
                var cartAdd = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("product_id",order.Dish.Id.ToString()),
                    new KeyValuePair<string, string>("quantity", order.Count.ToString())
                });
                SaveTodoItem("http://ravlyk.club/index.php?route=checkout/cart/add", cartAdd, client);              
            }


            var guestSave = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("customer_group_id","1"),
                    new KeyValuePair<string, string>("address_1",Adress),
                    new KeyValuePair<string, string>("firstname",UserName),
                    new KeyValuePair<string, string>("email",Email),
                    new KeyValuePair<string, string>("telephone",Phone),
                    new KeyValuePair<string, string>("city","Дрогобич")
                });
            SaveTodoItem("http://ravlyk.club/index.php?route=checkout/guest/save", guestSave, client);


            var payment = new FormUrlEncodedContent(new[]
               {
                    new KeyValuePair<string, string>("payment_method","cod"),
                    new KeyValuePair<string, string>("comment", "")
                });
            SaveTodoItem("http://ravlyk.club/index.php?route=checkout/payment_method/save", payment, client);

            UserDialogs.Instance.ShowSuccess("Ваше замовлення оформлено!");
            IoC.Get<OrderService>().ClearOrders();
            IoC.Get<INavigationService>().For<MainViewModel>().Navigate();
        }


        public void SaveTodoItem(string uri, FormUrlEncodedContent content, HttpClient client)
        {
            var result = client.PostAsync(uri, content).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;
        }

    }
}
