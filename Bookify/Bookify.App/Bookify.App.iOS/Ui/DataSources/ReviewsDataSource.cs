using System;
using Bookify.App.Core.ViewModels;
using Foundation;
using UIKit;

namespace Bookify.App.iOS.Ui.DataSources
{
    public class ReviewsDataSource : UITableViewSource
    {
        private readonly ReviewsViewModel viewModel;

        public ReviewsDataSource(ReviewsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return ReviewTableCell.CreateCell(tableView, indexPath, this.viewModel.Reviews[indexPath.Row]);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return this.viewModel.Reviews.Count;
            }
            return 0;
        }
    }
}