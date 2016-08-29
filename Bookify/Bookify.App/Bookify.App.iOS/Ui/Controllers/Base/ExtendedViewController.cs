using System;
using System.Threading.Tasks;
using Acr.UserDialogs;

using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Helpers;
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
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.CreateBindings();
        }

        protected abstract void CreateBindings();

        protected virtual T CreateViewModel()
        {
            return AppDelegate.Root.Resolve<T>();
        }

        protected override void Dispose(bool disposing)
        {
            this.ViewModel?.Dispose();
            base.Dispose(disposing);
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