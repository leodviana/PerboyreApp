using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class Exames : ContentPage
    {
        private View _title;

        public Exames()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            
            NavigationPage.SetTitleView(this,new ExamesTitleView());

        }

        

       
    }
}
