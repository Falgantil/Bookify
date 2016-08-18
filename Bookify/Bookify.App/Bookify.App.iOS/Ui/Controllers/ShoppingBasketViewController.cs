using System;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.DataSources;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class ShoppingBasketViewController : ExtendedViewController<ShoppingBasketViewModel>
    {
        public const string StoryboardIdentifier = "ShoppingBasketViewController";

        public ShoppingBasketViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.tblContent.Source = new ShoppingBasketDataSource(this.ViewModel);
            this.tblContent.RowHeight = 100;
            this.ViewModel.CartItems.CollectionChanged += async (sender, args) =>
            {
                await Task.Delay(500);
                this.InvokeOnMainThread(this.tblContent.ReloadData);
            };
        }

        protected override void CreateBindings()
        {

        }
    }
}