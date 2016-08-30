using System;
using System.Diagnostics;

using Bookify.App.Core.Initialization;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.Common.Models;

using CoreGraphics;

using Rope.Net;
using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class ReadBookViewController : ExtendedViewController<ReadBookViewModel>
    {
        public const string StoryboardIdentifier = "ReadBookViewController";

        public ReadBookViewController(IntPtr handle) : base(handle)
        {
        }

        private void BtnGoForward_Clicked(object sender, EventArgs e)
        {
            this.ViewModel.PageIndex = Math.Min(this.ViewModel.PageIndex + 1, this.ViewModel.PageCount - 1);
            this.LoadPage();
        }

        private void BtnGoBack_Clicked(object sender, EventArgs e)
        {
            this.ViewModel.PageIndex = Math.Max(this.ViewModel.PageIndex - 1, 0);
            this.LoadPage();
        }

        private async void LoadPage()
        {
            using (this.DialogService.Loading("Læser side..."))
            {
                var s = await this.ViewModel.LoadPage();
                this.webContent.LoadHtmlString(s, null);
            }
        }

        public override bool CanResignFirstResponder => false;

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.NavigationController.HidesBarsOnTap = true;

            await this.ViewModel.Initialize();
            this.ViewModel.PageIndex = 0;
            this.LoadPage();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.NavigationController.HidesBarsOnTap = false;
        }

        public override bool CanBecomeFirstResponder => true;

        public override UIView InputAccessoryView => this.CreateToolbar();

        private UIToolbar CreateToolbar()
        {
            var toolbar = new UIToolbar(new CGRect(0, 0, this.View.Frame.Width, 44));
            var btnGoBack = new UIBarButtonItem(UIBarButtonSystemItem.Rewind, this.BtnGoBack_Clicked);
            var btnGoForward = new UIBarButtonItem(UIBarButtonSystemItem.FastForward, this.BtnGoForward_Clicked);
            toolbar.SetItems(new[]
            {
                btnGoBack,
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                btnGoForward,
            }, false);

            btnGoBack.Bind(this.ViewModel, vm => vm.CanGoBack, (btn, enable) => btn.Enabled = enable);
            btnGoForward.Bind(this.ViewModel, vm => vm.CanGoForward, (btn, enable) => btn.Enabled = enable);
            return toolbar;
        }

        public DetailedBookDto Book { get; set; }

        public byte[] EpubBook { get; set; }

        protected override ReadBookViewModel CreateViewModel()
        {
            var paramEpub = new Parameter("epub", this.EpubBook);
            var paramBook = new Parameter("book", this.Book);
            return AppDelegate.Root.Resolve<ReadBookViewModel>(paramEpub, paramBook);
        }

        protected override void CreateBindings()
        {
            BindingCore.CreateBinding(this.ViewModel, this.ViewModel, vm => vm.Book, (vm, dto) => BindingIos.Apply(() =>
            {
                var navItem = this.NavigationItem;
                if (navItem == null)
                {
                    return;
                }
                navItem.Prompt = dto.Title;
            })).With(() => Debug.WriteLine("Disposing Title"));
            BindingCore.CreateBinding(this.ViewModel, this.ViewModel, vm => vm.PageIndex, (vm, index) => BindingIos.Apply(() =>
            {
                var navItem = this.NavigationItem;
                if (navItem == null)
                {
                    return;
                }
                navItem.Title = $"Side {index + 1}";
            })).With(() => Debug.WriteLine("Disposing Page Counter"));
        }
    }
}