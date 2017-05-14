using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Caliburn.Micro;
using Ravlyk.ViewModels;
using System.Collections.Generic;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Ravlyk.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        private readonly CaliburnAppDelegate appDelegate = new CaliburnAppDelegate();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
           
            LoadApplication(new App(IoC.Get<SimpleContainer>()));

            return base.FinishedLaunching(app, options);
        }
    }
}
