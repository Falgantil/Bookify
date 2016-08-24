using System;

using Acr.UserDialogs;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;

using MonoTouch.Dialog;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers.Base
{
    public class ExtendedDialogViewController<T> : DialogViewController
        where T : BaseViewModel
    {
        private readonly Lazy<IUserDialogs> dialogService = new Lazy<IUserDialogs>(() => AppDelegate.Root.Resolve<IUserDialogs>());

        protected IUserDialogs DialogService => this.dialogService.Value;

        public ExtendedDialogViewController(RootElement root) : base(root)
        {
            this.Initialize();
        }

        public ExtendedDialogViewController(UITableViewStyle style, RootElement root) : base(style, root)
        {
            this.Initialize();
        }

        public ExtendedDialogViewController(RootElement root, bool pushing) : base(root, pushing)
        {
            this.Initialize();
        }

        public ExtendedDialogViewController(UITableViewStyle style, RootElement root, bool pushing) : base(style, root, pushing)
        {
            this.Initialize();
        }

        public ExtendedDialogViewController(IntPtr handle) : base(handle)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.ViewModel = this.CreateViewModel();
        }

        public T ViewModel { get; private set; }
        
        protected virtual T CreateViewModel()
        {
            return AppDelegate.Root.Resolve<T>();
        }

        protected override void Dispose(bool disposing)
        {
            this.ViewModel.Dispose();
            base.Dispose(disposing);
        }

    }
}