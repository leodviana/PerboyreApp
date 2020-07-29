using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class DentistaPage : ContentPage,IDynamicTitle ,ITabPageIcons
    {
        private View _title;
        public DentistaPage()
        {
            InitializeComponent();
        }

        public string GetIcon()
        {
            return "home";
        }

        public string GetSelectedIcon()
        {
            return "home_selected";
        }

        public View GetTitle()
        {
            if (_title == null)
            {
                _title = new DentistaTitleView();
            }
            return _title;
        }
    }
}
