using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Caliburn.Micro;
using Android.Content.Res;
using Acr.UserDialogs;

namespace Ravlyk.Droid
{
    [Activity(Label = "Ravlyk", Icon = "@drawable/ic_launcher", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
          
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new App(IoC.Get<SimpleContainer>()));
            
        }
    }
}

