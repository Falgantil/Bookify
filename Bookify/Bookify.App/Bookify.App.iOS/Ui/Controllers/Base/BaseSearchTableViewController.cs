using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.App.iOS.Ui.Views;
using Bookify.Common.Models;
using Foundation;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public abstract class BaseSearchTableViewController : ExtendedTableViewController
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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            this.SelectedRow?.Invoke(this, this.GetBookModel(indexPath.Row));
            tableView.DeselectRow(indexPath, true);
        }

        public event EventHandler<BookDto> SelectedRow;
    }

    public abstract class ExtendedTableViewController : UITableViewController
    {
        private readonly Lazy<IUserDialogs> dialogService = new Lazy<IUserDialogs>(() => AppDelegate.Root.Resolve<IUserDialogs>());

        protected IUserDialogs DialogService => this.dialogService.Value;

        protected ExtendedTableViewController()
        {
        }

        protected ExtendedTableViewController(NSCoder coder) : base(coder)
        {
        }

        protected ExtendedTableViewController(NSObjectFlag t) : base(t)
        {
        }

        protected ExtendedTableViewController(IntPtr handle) : base(handle)
        {
        }

        protected ExtendedTableViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
        }

        protected ExtendedTableViewController(UITableViewStyle withStyle) : base(withStyle)
        {
        }

        protected async Task<T> TryTask<T>(Func<Task<T>> operation, string badRequest = null, string unauthorized = null, string notFound = null, string defaultMsg = null)
        {
            return await SharedMethods.TryTask(
                this.DialogService,
                operation,
                badRequest,
                unauthorized,
                notFound,
                defaultMsg);
        }

        protected async Task TryTask(Func<Task> operation, string badRequest = null, string unauthorized = null, string notFound = null, string defaultMsg = null)
        {
            await this.TryTask(async () =>
            {
                await operation();
                return true;
            },
                badRequest,
                unauthorized,
                notFound,
                defaultMsg);
        }
    }
}