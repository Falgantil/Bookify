using Foundation;
using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.Common.Models;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class GenresViewController : ExtendedViewController<GenresViewModel>
    {
        public const string StoryboardIdentifier = "GenresViewController";

        public GenresViewController(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateBindings()
        {
            this.View.Bind(
                this.ViewModel.Genres,
                collection => collection.IsLoading,
                (v, isLoading) => UIApplication.SharedApplication.NetworkActivityIndicatorVisible = isLoading);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new GenresDataSource(this.ViewModel);
            source.SelectedRow += this.SourceOnSelectedRow;
            this.tblContent.Source = source;
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            await this.TryTask(async () => await this.ViewModel.Genres.LoadMore());
        }

        private void SourceOnSelectedRow(object sender, GenreDto genreDto)
        {
            var storyboard = Ui.Storyboards.Storyboard.Main;
            var searchViewController = (SearchTableViewController)storyboard.InstantiateViewController(SearchTableViewController.StoryboardIdentifier);
            searchViewController.Genre = genreDto;
            this.NavigationController.PushViewController(searchViewController, true);
        }
    }

    public class GenresDataSource : UITableViewSource
    {
        private readonly GenresViewModel viewModel;

        public GenresDataSource(GenresViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler<GenreDto> SelectedRow;

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return section == 0 ? this.viewModel.Genres.Count : 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Row >= this.viewModel.Genres.Count - 1)
            {
                this.viewModel.Genres.LoadMore();
            }
            return GenreTableCell.CreateCell(tableView, indexPath, this.viewModel.Genres[indexPath.Row]);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            this.SelectedRow?.Invoke(this, this.viewModel.Genres[indexPath.Row]);
            tableView.DeselectRow(indexPath, true);
        }
    }
}