using System;

using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.TableCells;
using Bookify.Common.Models;

using Foundation;

using UIKit;

namespace Bookify.App.iOS.Ui.DataSources
{
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