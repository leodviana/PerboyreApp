using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PerboyreApp
{
    public partial class App : Application
    {
        public App()
        {
#if DEBUG
            HotReloader.Current.Run(this);
#endif
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
