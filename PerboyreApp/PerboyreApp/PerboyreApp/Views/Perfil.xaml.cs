using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace PerboyreApp.Views
{
    public partial class Perfil : ContentPage ,ITabPageIcons
    { 
       
        public Perfil()
        {
            InitializeComponent();
            // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
          
        }

        public string GetIcon()
        {
            return "perfil";
        }

        public string GetSelectedIcon()
        {
            return "perfil_selected";
        }
    }
}


