using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class Unidades : ContentPage, ITabPageIcons
    {
        public Unidades()
        {
            InitializeComponent();
        }

        public string GetIcon()
        {
            return "unidades";
        }

        public string GetSelectedIcon()
        {
            return "unidades";
        }
    }
}
