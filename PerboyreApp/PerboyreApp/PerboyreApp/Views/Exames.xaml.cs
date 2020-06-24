using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using PerboyreApp.Views.TitleViews;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class Exames : TabbedPage
    {
        private View _title;

        public Exames()
        {
            InitializeComponent();
           

            Children.Add(new imagensPage());
            Children.Add(new PdfPage());
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);

        }

        
        



    }
}
