using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerboyreApp.Controls;
using PerboyreApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace PerboyreApp.Views
{
    public partial class TesteImagem : ContentPage
    {
        public TesteImagem()
        {




           /* Content = new StackLayout
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                  new PDFView()
                  {
                    
                    IsPDF = true,
                    HeightRequest = 1000,
                    WidthRequest = 1000,
                    VerticalOptions = LayoutOptions.FillAndExpand
                  }
                }
            };
        
           */

            InitializeComponent();

            
            if (Device.RuntimePlatform == Device.Android)
            {
                pdfView.Uri = "";
                pdfView.On<Android>().EnableZoomControls(true);
                pdfView.On<Android>().DisplayZoomControls(false);
            }

           else
                pdfView.Source = "";
        }

        

    }
}
