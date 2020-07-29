using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class imagensPage : ContentPage ,ITabPageIcons
    {
        public imagensPage()
        {
            InitializeComponent();
        }

        public string GetIcon()
        {
            return "exames";
        }

        public string GetSelectedIcon()
        {
            return "exames_selected";
        }
    }
}
