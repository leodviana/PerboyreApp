using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace PerboyreApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            /*UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(76, 5, 7);

            // Color of the tabbar background:
            UITabBar.Appearance.BarTintColor = UIColor.FromRGB(247, 247, 247);

            // Color of the selected tab text color:
            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes()
                {
                    TextColor = UIColor.FromRGB(76, 5, 7)
                },
                UIControlState.Selected);

            // Color of the unselected tab icon & text:
            UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes()
                {
                    TextColor = UIColor.Gray
                },
                UIControlState.Normal);*/
            Stormlion.PhotoBrowser.iOS.Platform.Init();
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            global:: Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
    public class iOsInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
