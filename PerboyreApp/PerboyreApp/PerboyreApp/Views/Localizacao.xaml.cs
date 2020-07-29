using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace PerboyreApp.Views
{
    public partial class Localizacao : ContentPage, ITabPageIcons
    {
        public Localizacao()
        {
            InitializeComponent();
            
        }

        public string GetIcon()
        {
            return "requisicao";
        }

        public string GetSelectedIcon()
        {
            return "requisicao_selected";
        }
    }
}
