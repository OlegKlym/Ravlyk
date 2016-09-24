using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ravlyk.Services
{
    public  class Settings
    {
        private  ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string UserNameKey = "username_key";
        private  readonly string UserNameDefault = string.Empty;

        private const string AdressKey = "adress_key";
        private readonly string AdressDefault = string.Empty;

        private const string EmailKey = "email_key";
        private readonly string EmailDefault = string.Empty;

        private const string PhoneKey = "phone_key";
        private readonly string PhoneDefault = string.Empty;
        #endregion

        public  string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserNameKey, value.ToString()); }
        }

        public string Adress
        {
            get { return AppSettings.GetValueOrDefault<string>(AdressKey, AdressDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AdressKey, value.ToString()); }
        }

        public string Email
        {
            get { return AppSettings.GetValueOrDefault<string>(EmailKey, EmailDefault); }
            set { AppSettings.AddOrUpdateValue<string>(EmailKey, value.ToString()); }
        }

        public string Phone
        {
            get { return AppSettings.GetValueOrDefault<string>(PhoneKey, PhoneDefault); }
            set { AppSettings.AddOrUpdateValue<string>(PhoneKey, value.ToString()); }
        }
    }
}
