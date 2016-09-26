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
            //Text = "Дана функція поки  в розробці";
            _customer.customer_group_id = 1;
            _customer.address_1 = Adress;
            _customer.firstname = UserName;
            _customer.email = Email;
            _customer.telephone = Phone;
            _customer.city = "Дрогобич";
            var uri = "http://ravlyk.club/index.php?route=checkout/cart/add ";
            SaveTodoItemAsync(uri);
        }

        
        public async Task SaveTodoItemAsync(string uri)
        {
            
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new OrderModel()
                {
                    Count = 1
                });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                var result = await response.Content.ReadAsStringAsync();

            }

        }

    }
}
