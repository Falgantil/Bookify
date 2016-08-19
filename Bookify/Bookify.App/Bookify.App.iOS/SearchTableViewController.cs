using Foundation;
using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class SearchTableViewController : BaseSearchTableViewController, IUISearchResultsUpdating
    {
        public const string StoryboardIdentifier = "SearchTableViewController";

        private UISearchController searchController;
        private ResultsTableController resultsTableController;

        public SearchTableViewController(IntPtr handle) : base(handle)
        {
        }

        public SearchViewModel ViewModel { get; private set; }

        public GenreDto Genre { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.ViewModel = AppDelegate.Root.Resolve<SearchViewModel>();
            this.SelectedRow += this.OnSelectedRow;

            this.resultsTableController = new ResultsTableController(this.ViewModel);
            this.resultsTableController.SelectedRow += this.OnSelectedRow;

            this.searchController = new UISearchController(this.resultsTableController)
            {
                WeakDelegate = this,
                DimsBackgroundDuringPresentation = false,
                SearchResultsUpdater = this
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this.Genre != null)
            {
                this.NavigationItem.Prompt = $"Søger i {this.Genre.Name}";
                this.ViewModel.Genre = this.Genre;
            }
            this.ViewModel.RefreshContent();
        }

        private async void OnSelectedRow(object sender, BookDto bookDto)
        {
            DetailedBookDto book;
            using (this.DialogService.Loading("Henter bog..."))
            {
                book = await this.TryTask(async () => await this.ViewModel.GetBook(bookDto.Id),
                    null,
                    null,
                    "Bogen kunne ikke hentes");
                if (book == null)
                {
                    return;
                }
            }
            var storyboard = Ui.Storyboards.Storyboard.Main;
            var viewController = (BookOverviewViewController)storyboard.InstantiateViewController(BookOverviewViewController.StoryboardIdentifier);
            viewController.Book = book;
            this.NavigationController.PushViewController(viewController, true);
        }

        protected override int GetBookCount()
        {
            return this.ViewModel.Filtered.Count;
        }

        protected override BookDto GetBookModel(int index)
        {
            return this.ViewModel.Filtered[index];
        }

        [Export("searchBarSearchButtonClicked:")]
        public virtual void SearchButtonClicked(UISearchBar searchBar)
        {
            searchBar.ResignFirstResponder();
        }

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

        protected override BookDto GetBookModel(int index)
        {
            return this.viewModel.Filtered[index];
        }
    }
}