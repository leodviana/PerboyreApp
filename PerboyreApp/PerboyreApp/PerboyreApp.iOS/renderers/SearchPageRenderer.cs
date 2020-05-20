﻿using System;
using PerboyreApp.Interfaces;
using PerboyreApp.iOS.renderers;
using PerboyreApp.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Dentista), typeof(SearchPageRenderer))]
namespace PerboyreApp.iOS.renderers
{
    public class SearchPageRenderer : PageRenderer, IUISearchResultsUpdating
    {
        readonly UISearchController _searchController;

        public SearchPageRenderer()
        {
            _searchController = new UISearchController(searchResultsController: null)
            {
                SearchResultsUpdater = this,
                DimsBackgroundDuringPresentation = false,
                HidesNavigationBarDuringPresentation = false,
                HidesBottomBarWhenPushed = true
            };
            _searchController.SearchBar.Placeholder = "Selecione o Dentista";
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ParentViewController.NavigationItem.SearchController is null)
            {
                ParentViewController.NavigationItem.SearchController = _searchController;
                DefinesPresentationContext = true;

                //Work-around to ensure the SearchController appears when the page first appears https://stackoverflow.com/a/46313164/5953643
                ParentViewController.NavigationItem.SearchController.Active = true;
                ParentViewController.NavigationItem.SearchController.Active = false;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ParentViewController.NavigationItem.SearchController = null;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            if (Element is ISearchPage searchPage)
                searchPage.OnSearchBarTextChanged(searchController.SearchBar.Text);
        }
    }

}
