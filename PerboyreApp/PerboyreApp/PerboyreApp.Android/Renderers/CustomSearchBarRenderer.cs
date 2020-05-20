using System;
using Android.Views;
using Android.Widget;
using PerboyreApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.Content;


[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace PerboyreApp.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                RemoveMagnifierIcon();
                RemoveBottomLine();
                ChangeBackgroundDrawable();
            }
        }

        private void RemoveMagnifierIcon()
        {
            var searchMagIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            var searchMagIconView = Control.FindViewById<ImageView>(searchMagIconId);
            searchMagIconView.SetImageDrawable(null);
            searchMagIconView.Visibility = ViewStates.Gone;
        }

        private void RemoveBottomLine()
        {
            var plateId = Context.Resources.GetIdentifier("android:id/search_plate", null, null);
            var plateView = Control.FindViewById<LinearLayout>(plateId);
            plateView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

        private void ChangeBackgroundDrawable()
        {
            var customBackgroundId = Resource.Drawable.custom_search_background;
            Control.Background = Context.GetDrawable(customBackgroundId);
        }

    }
}
