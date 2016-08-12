using System;

using Acr.UserDialogs;

using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;

using Foundation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers.Base
{
    public abstract class ExtendedViewController<T> : UIViewController 
        where T : BaseViewModel
    {
        private readonly Lazy<IUserDialogs> dialogService = new Lazy<IUserDialogs>(() => AppDelegate.Root.Resolve<IUserDialogs>());

        protected IUserDialogs DialogService => this.dialogService.Value;

        protected ExtendedViewController()
        {
        }

        protected ExtendedViewController(NSCoder coder)
            : base(coder)
        {
        }

        protected ExtendedViewController(NSObjectFlag t)
            : base(t)
        {
        }

        protected ExtendedViewController(IntPtr handle)
            : base(handle)
        {
        }

        protected ExtendedViewController(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        public T ViewModel { get; private set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.ViewModel = this.CreateViewModel();
            this.CreateBindings();
        }

        protected abstract void CreateBindings();

        protected virtual T CreateViewModel()
        {
            return AppDelegate.Root.Resolve<T>();
        }
    }
}