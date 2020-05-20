using System;
using PerboyreApp.Interfaces;
using PerboyreApp.ViewModels;
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

           // NavigationPage.SetTitleView(this, new DentistaTitleView());
           // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Always);
        }

        

        public View GetTitle()
        {
            if (_title==null)
            {
                _title = new DentistaTitleView();
            }
            return _title;
        }

        
    }
}
