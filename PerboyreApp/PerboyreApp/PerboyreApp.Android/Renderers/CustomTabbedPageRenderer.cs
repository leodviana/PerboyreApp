﻿using System;
using System.Linq;
using Android.Content;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using PerboyreApp.Droid.Renderers;
using PerboyreApp.Droid.Utils;
using PerboyreApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace PerboyreApp.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        private TabbedPage _formsTabs;
        private BottomNavigationView _bottomNavigationView;

        public CustomTabbedPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var altura = (int)Resources.DisplayMetrics.HeightPixels;
                int tamamnhoicone = 90;
                int alturaTab = 100;
                if (altura<=976)
                {
                    tamamnhoicone = 50;
                    alturaTab = 60;
                }
                _formsTabs = Element;
                _formsTabs.CurrentPageChanged += OnCurrentPageChanged;

                var relativeLayout = base.GetChildAt(0) as Android.Widget.RelativeLayout;
                
                    _bottomNavigationView = relativeLayout.GetChildAt(1) as BottomNavigationView;
                    _bottomNavigationView.ItemIconTintList =null;
                    _bottomNavigationView.SetMinimumHeight(alturaTab); 
                    _bottomNavigationView.ItemIconSize = tamamnhoicone;
                    _bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;

                    UpdateAllTabs();
                
                
               
            }

            if (e.OldElement != null)
            {
                _formsTabs.CurrentPageChanged -= OnCurrentPageChanged;
            }
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            UpdateAllTabs();
        }

        private void UpdateAllTabs()
        {
            for (var index = 0; index < _formsTabs.Children.Count; index++)
            {
                var androidTab = _bottomNavigationView.Menu.GetItem(index);
                int iconId;

                //if (_formsTabs.Children[index]?.Navigation?.NavigationStack?.FirstOrDefault() is ITabPageIcons tabPage)
                if (_formsTabs.Children[index] is ITabPageIcons tabPage)
                {
                    if (_formsTabs.Children[index] == _formsTabs.CurrentPage)
                    {
                        iconId = ResourceUtil.GetDrawableIdByFileName(tabPage.GetSelectedIcon(), Context);
                        androidTab.SetIcon(iconId);
                        continue;
                    }

                    iconId = ResourceUtil.GetDrawableIdByFileName(tabPage.GetIcon(), Context);
                    androidTab.SetIcon(iconId);
                    continue;
                }
            }
        }
    }
}
