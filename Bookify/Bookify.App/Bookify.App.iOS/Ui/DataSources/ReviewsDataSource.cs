using System;
using Bookify.App.Core.Models;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.TableCells;

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
            var dto = this.viewModel.Reviews[indexPath.Row];
            var reviewModel = new ReviewModel
            {
                Id = dto.BookId,
                PersonName = dto.PersonName,
                Message = dto.Message,
                Rating = dto.Rating,
                PersonId = dto.PersonId
            };
            return ReviewTableCell.CreateCell(tableView, indexPath, reviewModel);
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