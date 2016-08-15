using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            var iconMore = UIImage.FromBundle("Icons/More.png");
            var btnMore = new UIBarButtonItem(iconMore, UIBarButtonItemStyle.Plain, this.BtnMore_Click);
            this.ParentViewController.NavigationItem.SetRightBarButtonItem(btnMore, false);
        }

        private async void BtnMore_Click(object sender, EventArgs e)
        {
            const string MsgTitle = "Valgmuligheder";

            const string OptCancel = "Annuller";
            const string OptAddToBasket = "Tilføj til indkøbskurv";
            const string OptReadBook = "Læs bog";
            const string OptBorrowBook = "Lån bog";

            List<string> options = new List<string> { OptAddToBasket };
            if (this.ViewModel.OwnsBook)
            {
                options.Add(OptReadBook);
            }
            else if (this.ViewModel.Borrowable)
            {
                options.Add(OptBorrowBook);
            }
            var result = await this.DialogService.ActionSheetAsync(MsgTitle, OptCancel, null, null, options.ToArray());
            switch (result)
            {
                case OptCancel:
                    return;
                case OptAddToBasket:
                    this.AddToBasket_Clicked();
                    break;
                case OptReadBook:
                    this.ReadBook_Clicked();
                    break;
                case OptBorrowBook:
                    this.BorrowBook_Clicked();
                    break;
            }
        }

        private async void AddToBasket_Clicked()
        {
            await this.ViewModel.AddToBasket();
        }

        private void ReadBook_Clicked()
        {
            
        }

        private void BorrowBook_Clicked()
        {
            
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