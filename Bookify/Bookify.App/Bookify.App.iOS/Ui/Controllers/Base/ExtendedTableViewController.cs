using System;
using System.Threading.Tasks;

using Acr.UserDialogs;

using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Helpers;

using Foundation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers.Base
{
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