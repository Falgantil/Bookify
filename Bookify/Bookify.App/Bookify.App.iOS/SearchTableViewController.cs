using Foundation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Bookify.App.Core.Models;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers;
using Bookify.Models;
using Rope.Net.iOS;
using UIKit;
using Book = Bookify.Models.Book;

namespace Bookify.App.iOS
{
    public partial class SearchTableViewController : BaseSearchTableViewController
    {
        private UISearchController searchController;
        private ResultsTableController resultsTableController;

        public SearchTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewModel = AppDelegate.Root.Resolve<SearchViewModel>();

            this.resultsTableController = new ResultsTableController(this.ViewModel);

            this.searchController = new UISearchController(this.resultsTableController)
            {
                WeakDelegate = this,
                DimsBackgroundDuringPresentation = false,
                WeakSearchResultsUpdater = this
            };

            this.searchController.SearchBar.SizeToFit();
            this.TableView.TableHeaderView = this.searchController.SearchBar;

            this.resultsTableController.TableView.WeakDelegate = this;
            this.searchController.SearchBar.WeakDelegate = this;

            this.DefinesPresentationContext = true;

            this.searchController.SearchBar.Bind(this.ViewModel, vm => vm.SearchText,
                (searchbar, searchText) => searchbar.Text = searchText);

            this.ViewModel.Filtered.CollectionChanged += (sender, args) =>
            {
                this.InvokeOnMainThread(() =>
                {
                    this.TableView.ReloadData();
                    this.resultsTableController.TableView.ReloadData();
                });
            };
        }

        public SearchViewModel ViewModel { get; private set; }

        protected override int GetBookCount()
        {
            return this.ViewModel.Filtered.Count;
        }

        protected override Book GetBookModel(int index)
        {
            return this.ViewModel.Filtered[index];
        }

        [Export("searchBarSearchButtonClicked:")]
        public virtual void SearchButtonClicked(UISearchBar searchBar)
        {
            searchBar.ResignFirstResponder();
        }

        [Export("updateSearchResultsForSearchController:")]
        public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            this.ViewModel.SearchText = searchController.SearchBar.Text;
        }
    }

    public class ResultsTableController : BaseSearchTableViewController
    {
        private readonly SearchViewModel viewModel;

        public ResultsTableController(SearchViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        protected override int GetBookCount()
        {
            return this.viewModel.Filtered.Count;
        }

        protected override Book GetBookModel(int index)
        {
            return this.viewModel.Filtered[index];
        }
    }
}