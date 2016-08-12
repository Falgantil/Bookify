using System;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.Models;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.Helpers;
using CoreAnimation;
using CoreGraphics;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class BookSummaryViewController : ExtendedViewController<BookSummaryViewModel>
    {
        public BookSummaryViewController(IntPtr handle) : base(handle)
        {
        }

        public BookModel Book { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CAGradientLayer gradient = new CAGradientLayer
            {
                Frame = this.viewTitleBackground.Bounds,
                Colors = new[] { UIColor.Clear.CGColor, UIColor.Black.CGColor },
                StartPoint = new CGPoint(0.5, 0.0),
                EndPoint = new CGPoint(0.5, 1.0)
            };
            this.viewTitleBackground.BackgroundColor = UIColor.Clear;
            this.viewTitleBackground.Layer.InsertSublayer(gradient, 0);

            this.lblSummary.UserInteractionEnabled = true;
            this.lblSummary.Enabled = true;
            //this.lblSummary.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            //{
            //    var value = this.constImageHeight.Constant == 200 ? 0 : 200;
            //    this.constImageHeight.Constant = value;
            //    UIView.Animate(.5f, () =>
            //    {
            //        this.View.LayoutIfNeeded();
            //    });
            //}));

            var iconMore = UIImage.FromBundle("Icons/More.png");
            var btnMore = new UIBarButtonItem(iconMore, UIBarButtonItemStyle.Plain, this.BtnMore_Click);
            this.ParentViewController.NavigationItem.SetRightBarButtonItem(btnMore, false);
        }

        private void BtnMore_Click(object sender, EventArgs e)
        {
            this.DialogService.ActionSheetAsync(string.Empty, "Annuller", null, null, "Køb bog", "Anmeldinger");
        }

        protected override void CreateBindings()
        {
            this.imgBookCover.BindImageUrl(this.ViewModel, vm => vm.Book.CoverUrl);
            this.lblBookTitle.BindText(this.ViewModel, vm => vm.Book.Title);
            this.lblAuthor.BindText(this.ViewModel, vm => vm.Book.Author, "af {0}");
            this.lblChapters.Bind(
                this.ViewModel,
                vm => vm.Book.Chapters,
                (lbl, val) =>
                    {
                        switch (val)
                        {
                            case 0:
                                lbl.Text = "Ingen kapitler";
                                break;
                            case 1:
                                lbl.Text = "1 kapitel";
                                break;
                            default:
                                lbl.Text = val.ToString() + " kapitler";
                                break;
                        }
                    });
            this.lblSummary.BindText(this.ViewModel, vm => vm.Book.Summary);
        }

        protected override BookSummaryViewModel CreateViewModel()
        {
            return AppDelegate.Root.Resolve<BookSummaryViewModel>(new Parameter("book", this.Book));
        }
    }
}