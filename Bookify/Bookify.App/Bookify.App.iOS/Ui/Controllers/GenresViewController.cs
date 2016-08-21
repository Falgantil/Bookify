using System;

using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;
using Bookify.Common.Models;

using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
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
            this.ViewModel.Genres.CollectionChanged += (s, e) => this.InvokeOnMainThread(this.tblContent.ReloadData);
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
}