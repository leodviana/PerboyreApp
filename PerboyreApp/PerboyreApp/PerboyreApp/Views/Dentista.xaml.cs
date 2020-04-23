using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class Dentista : ContentPage , IDynamicTitle
    {
        private View _title;
        public Dentista()
        {
            InitializeComponent();
        }

        public View GetTitle()
        {
            if (_title == null)
                _title = new DentistaTitleView();
            return _title;

        }
    }
}
