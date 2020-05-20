using System;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace PerboyreApp.Navegacao
{
    public class CustomNavigationPage :Xamarin.Forms.NavigationPage
    {
        public CustomNavigationPage()
        {
           On<iOS>().SetPrefersLargeTitles(true);
        }
    }
}
