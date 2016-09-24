using Caliburn.Micro;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ravlyk.Services;
using Android.Content.Res;

namespace Ravlyk.ViewModels
{

    public class FormViewModel : Caliburn.Micro.Screen
    {

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

    }
}
