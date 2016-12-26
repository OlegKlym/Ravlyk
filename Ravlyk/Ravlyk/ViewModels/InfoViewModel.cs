using Acr.UserDialogs;
using Caliburn.Micro;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class InfoViewModel :Screen
    {
        public ICommand PhoneCallCommand { set; get; }
        public ICommand CallOrderCommand { set; get; }

        private string _name;
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
         }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public InfoViewModel()
        {
            PhoneCallCommand = new Command(PhoneCall);
            CallOrderCommand = new Command(CallOrder);
        }

        public void PhoneCall(object orderObject)
        {
            string number = orderObject.ToString();
            Device.OpenUri(new Uri("tel:" + number));
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public async void CallOrder()
        {
            var result = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                Placeholder = "Номер телефону",
                Title = "Замовити дзвінок",               
                OkText = "Замовити",
                CancelText = "Скасувати"
            });                              
            if (result.Ok) {
                Phone = result.Text;
            }
          
        }

        
    }
}
