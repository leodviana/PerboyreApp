using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace PerboyreApp.Views
{
    public partial class PacientesPage : ContentPage 
    {
        private View _title;

        public PacientesPage()
        {
            InitializeComponent();

            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
           // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Always);
        //    Xamarin.Forms.NavigationPage.SetTitleView(this, new PacienteTitleView());
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }




    }
}
