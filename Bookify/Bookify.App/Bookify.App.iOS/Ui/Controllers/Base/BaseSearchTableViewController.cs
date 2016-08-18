using System;
using Bookify.App.iOS.Ui.Views;
using Bookify.Common.Models;
using Foundation;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public abstract class BaseSearchTableViewController : UITableViewController
    {
        protected BaseSearchTableViewController()
        {
        }

        protected BaseSearchTableViewController(NSCoder coder) : base(coder)
        {
        }

        protected BaseSearchTableViewController(NSObjectFlag t) : base(t)
        {
        }

        protected BaseSearchTableViewController(IntPtr handle) : base(handle)
        {
        }

        protected BaseSearchTableViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected BaseSearchTableViewController(UITableViewStyle withStyle) : base(withStyle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.TableView.RegisterNibForCellReuse(BookTableCell.Nib, BookTableCell.Key);
            this.TableView.RowHeight = 100;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return BookTableCell.CreateCell(tableView, indexPath, this.GetBookModel(indexPath.Row));
        }

        public override nint RowsInSection(UITableView tableView, nint section) => section == 0 ? this.GetBookCount() : 0;

        public override nint NumberOfSections(UITableView tableView) => 1;

        protected abstract int GetBookCount();

        protected abstract BookDto GetBookModel(int index);
    }
}