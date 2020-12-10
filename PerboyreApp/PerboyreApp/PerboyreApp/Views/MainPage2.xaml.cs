using System;
using System.Collections.Generic;
using System.ComponentModel;
using PerboyreApp.Interfaces;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage2 : TabbedPage
    {
        public MainPage2()
        {
            InitializeComponent();

            Children.Add(new DentistaPage());
            //Children.Add(new Localizacao());

            //if (Device.RuntimePlatform == Device.iOS)
            //    Children.Add(new TrainingView());

            Children.Add(new Unidades());
            Children.Add(new Perfil());
         }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (CurrentPage is IDynamicTitle page)
            {

                NavigationPage.SetHasNavigationBar(this, true);
                
                NavigationPage.SetTitleView(this, page.GetTitle());
                return;
            }

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
