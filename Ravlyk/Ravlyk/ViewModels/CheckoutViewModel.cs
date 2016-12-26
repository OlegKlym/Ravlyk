using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ravlyk.Services;
using System.Windows.Input;
using Caliburn.Micro.Xamarin.Forms;
using System.Net.Http;
using System.Collections.Generic;
using Acr.UserDialogs;
using System.Net.Http.Headers;
using Ravlyk.Helpers;
using Plugin.Connectivity;

namespace Ravlyk.ViewModels
{

    public class CheckoutViewModel : Screen
    {
        public ICommand ConfirmCommand { set; get; }
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

        public CheckoutViewModel(INavigationService navigationService)
        {
            ConfirmCommand = new Command(ConfirmAsync);
            _navigationService = navigationService;
        }

        public async void ConfirmAsync()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Host = "ravlyk.club";
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                client.DefaultRequestHeaders.Add("Origin", "http://ravlyk.club");
                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                client.DefaultRequestHeaders.Add("Cookie", "language=en");
                client.DefaultRequestHeaders.Add("Cookie", "PHPSESSID=" + Guid.NewGuid().ToString());
                client.DefaultRequestHeaders.Add("Cookie", "language=en");
                client.DefaultRequestHeaders.Add("Cookie", "currency=USD");
               

                var orders = IoC.Get<OrderService>().GetOrders();
                foreach (var order in orders)
                {
                    var cartAdd = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("product_id",order.Dish.Id.ToString()),
                        new KeyValuePair<string, string>("quantity", order.Count.ToString())    
                    });
                    await SaveTodoItemAsync("http://ravlyk.club/index.php?route=checkout/cart/add", cartAdd, client);                  
                }


                var response = await client.GetAsync("http://ravlyk.club/index.php?route=checkout/checkout/customfield&customer_group_id=1");
                var resultContent = await response.Content.ReadAsStringAsync();


                var guestSave = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("customer_group_id","1"),
                    new KeyValuePair<string, string>("address_1",Adress),
                    new KeyValuePair<string, string>("firstname",UserName),
                    new KeyValuePair<string, string>("email",Email),
                    new KeyValuePair<string, string>("telephone",Phone),
                    new KeyValuePair<string, string>("city","Дрогобич")
                });
                await SaveTodoItemAsync("http://ravlyk.club/index.php?route=checkout/guest/save", guestSave, client);
               

                var payment = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("payment_method","cod"),
                    new KeyValuePair<string, string>("comment", "None")
                });
                //SaveTodoItem("http://ravlyk.club/index.php?route=checkout/payment_method/save", payment, client);
                var result = await client.PostAsync("http://ravlyk.club/index.php?route=checkout/cart/add", payment);
                if (result.IsSuccessStatusCode)
                {
                    var resultContent3 = await result.Content.ReadAsStringAsync();
                }


                var unixTime = IoC.Get<Helper>().GetGMTInMS();
                response = await client.GetAsync("http://ravlyk.club/index.php?route=payment/cod/confirm&_=" + unixTime.ToString());
                var time = await response.Content.ReadAsStringAsync();


                IoC.Get<OrderService>().ClearOrders();
                UserDialogs.Instance.ShowSuccess("Ваше замовлення оформлено!");
                await IoC.Get<INavigationService>().GoBackToRootAsync();

            }
            catch
            {
                UserDialogs.Instance.Alert("Відсутнє з'єднання з інтернетом!");
            }
        }

        public async Task SaveTodoItemAsync(string uri, FormUrlEncodedContent content, HttpClient client)
        {
            var result = await client.PostAsync(uri, content);
            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
            }
        }

        protected override void OnActivate()
        {
            if (CrossConnectivity.Current.IsConnected)
                base.OnActivate();
            else
                UserDialogs.Instance.Alert("Відсутнє з'єднання з інтернетом!");
        }
    }
}
