using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Caliburn.Micro;

namespace Ravlyk.Droid
{
    [Activity(Label = "Ravlyk", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            LoadApplication(new App(IoC.Get<SimpleContainer>()));
        }
    }
}

