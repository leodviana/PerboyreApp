using System;
using System.Collections.Generic;
using PerboyreApp.Interfaces;
using Xamarin.Forms;

namespace PerboyreApp.Views
{
    public partial class PdfPage : ContentPage, ITabPageIcons
    {
        public PdfPage()
        {
            InitializeComponent();
        }

        public string GetIcon()
        {
            return "pdf";
        }

        public string GetSelectedIcon()
        {
            return "pdf_selected";
        }
    }
}
