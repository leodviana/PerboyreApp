using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class Unidades : ContentPage, IDynamicTitle,ITabPageIcons
    {
        private View _title;
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
            return "unidades_selected";
        }

        public View GetTitle()
        {
            if (_title == null)
            {
                _title = new UnidadesTitleView();
            }
            return _title;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}
