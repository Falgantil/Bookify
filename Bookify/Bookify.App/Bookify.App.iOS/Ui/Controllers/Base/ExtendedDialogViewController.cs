using System;
using Acr.UserDialogs;
using Bookify.App.iOS.Initialization;
using MonoTouch.Dialog;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class ExtendedDialogViewController : DialogViewController
    {
        private readonly Lazy<IUserDialogs> dialogService = new Lazy<IUserDialogs>(() => AppDelegate.Root.Resolve<IUserDialogs>());

        protected IUserDialogs DialogService => this.dialogService.Value;

        public ExtendedDialogViewController(RootElement root) : base(root)
        {
        }

        public ExtendedDialogViewController(UITableViewStyle style, RootElement root) : base(style, root)
        {
        }

        public ExtendedDialogViewController(RootElement root, bool pushing) : base(root, pushing)
        {
        }

        public ExtendedDialogViewController(UITableViewStyle style, RootElement root, bool pushing) : base(style, root, pushing)
        {
        }

        public ExtendedDialogViewController(IntPtr handle) : base(handle)
        {
        }
    }
}