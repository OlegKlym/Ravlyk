using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ravlyk.ViewModels
{
    public class MenuViewModel
    {

        public ICommand PhoneCallCommand { protected set; get; }

        public MenuViewModel()
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
