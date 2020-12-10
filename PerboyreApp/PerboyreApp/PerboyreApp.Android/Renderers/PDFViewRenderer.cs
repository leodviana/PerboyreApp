using System;
using Android.Content;
using PerboyreApp.Controls;
using PerboyreApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(PDFView), typeof(PDFViewRenderer))]
namespace PerboyreApp.Droid.Renderers
{
    
        public class PDFViewRenderer : WebViewRenderer
        {
            public PDFViewRenderer(Context context) : base(context)
            {
            }
            protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
            {
                base.OnElementChanged(e);

                if (e.NewElement != null)
                {
                    Element.Navigated += Element_Navigated;

                    var pdfView = Element as PDFView;
                    Control.Settings.AllowUniversalAccessFromFileURLs = true;
                    if (pdfView.IsPDF)
                    {
                        var url = "https://drive.google.com/viewerng/viewer?embedded=true&url=" + pdfView.Uri;
                        ///var url = "https://docs.google.com/gview?embedded=true&url=" + pdfView.Uri;

                        Control.LoadUrl(url);
                    }
                    else
                    {
                        Control.LoadUrl(pdfView.Uri);
                    }

                }
            }

            private void Element_Navigated(object sender, WebNavigatedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(Control.Title))
                {
                    Control.Reload();
                }
            }
        }
    
}
