﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Foundation;
using PerboyreApp.iOS.renderers;
using PerboyreApp.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Dentista), typeof(SearchViewRenderer))]
namespace PerboyreApp.iOS.renderers
{
    public class SearchViewRenderer : PageRenderer, IUISearchBarDelegate
    {
        private UIViewController _searchResultsController;
        private UISearchController _searchController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

           // var storyboard = UIStoryboard.FromName("SearchResults", null);
           // _searchResultsController = storyboard.InstantiateInitialViewController();

            _searchController = new UISearchController(_searchResultsController)
            {
                AutomaticallyShowsCancelButton = false
            };
           /* _searchController.SearchBar.Delegate = this;

            if (Element.BindingContext is SearchViewModel viewModel)
                viewModel.Suggestions.CollectionChanged += OnSuggestionsChanged;

            if (_searchResultsController is SearchResultsViewController viewController)
                viewController.SuggestionSelected += OnSuggestionSelected;*/
        }

        /*private void OnSuggestionsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Reset)
            {
                if (_searchResultsController is SearchResultsViewController viewController)
                {
                    viewController.ClearResults();
                }
            }

            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                if (_searchResultsController is SearchResultsViewController viewController)
                {
                    viewController.UpdateResults(args.NewItems);
                }
            }
        }

        private void OnSuggestionSelected(object sender, SearchSuggestion suggestion)
        {
            if (Element.BindingContext is SearchViewModel viewModel)
                viewModel.SelectSuggestion(suggestion);
        }

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            base.WillMoveToParentViewController(parent);

            parent.NavigationItem.SearchController = _searchController;
            parent.NavigationItem.HidesSearchBarWhenScrolling = false;
        }

        [Export("searchBar:textDidChange:")]
        public void TextChanged(UISearchBar searchBar, string searchText)
        {
            if (Element is SearchView searchView)
                searchView.SearchBy(searchText);
        }*/
    }
}
