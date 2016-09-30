using Caliburn.Micro;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class InfoViewModel :Screen
    {
        public ICommand PhoneCallCommand { set; get; }

        public InfoViewModel()
        {
            PhoneCallCommand = new Command(PhoneCall);
        }

        public void PhoneCall(object orderObject)
        {
            string number = orderObject.ToString();
            Device.OpenUri(new Uri("tel:" + number));           
        }
    }
}
