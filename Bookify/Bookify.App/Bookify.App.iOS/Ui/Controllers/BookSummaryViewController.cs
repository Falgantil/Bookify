using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.Common.Models;
using CoreAnimation;
using CoreGraphics;
using EpubReader.Net.Core;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class BookSummaryViewController : ExtendedViewController<BookSummaryViewModel>
    {
        private UIBarButtonItem btnMore;

        public BookSummaryViewController(IntPtr handle) : base(handle)
        {
        }

        public DetailedBookDto Book { get; set; }

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

            this.btnMore = new UIBarButtonItem(UIImage.FromBundle("Icons/More.png"), UIBarButtonItemStyle.Plain, this.BtnMore_Click);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.ParentViewController.NavigationItem.SetRightBarButtonItem(this.btnMore, false);
        }

        private async void BtnMore_Click(object sender, EventArgs e)
        {
            const string MsgTitle = "Valgmuligheder";

            const string OptCancel = "Annuller";
            const string OptAddToCart = "Tilf�j til indk�bskurv";
            const string OptReadBook = "L�s bog";
            const string OptBorrowBook = "L�n bog";

            List<string> options = new List<string> { OptAddToCart };
            //if (this.ViewModel.OwnsBook)
            //{
            options.Add(OptReadBook);
            //}
            //else 
            if (this.ViewModel.IsLoggedIn)
            {
                options.Add(OptBorrowBook);
            }
            var result = await this.DialogService.ActionSheetAsync(MsgTitle, OptCancel, null, null, options.ToArray());
            switch (result)
            {
                case OptCancel:
                    return;
                case OptAddToCart:
                    this.AddToCart_Clicked();
                    break;
                case OptReadBook:
                    this.ReadBook_Clicked();
                    break;
                case OptBorrowBook:
                    this.BorrowBook_Clicked();
                    break;
            }
        }

        private async void AddToCart_Clicked()
        {
            await this.ViewModel.AddToCart();
        }

        private async void ReadBook_Clicked()
        {
            byte[] bookEpub;
            using (this.DialogService.Loading("Henter bog..."))
            {
                bookEpub = await this.TryTask(async () => await this.ViewModel.DownloadBook(),
                    "Foresp�rgslen kunne ikke blive behandlet p� serveren",
                    "Du har ikke tilladelse til at l�se denne bog",
                    "Bogen blev ikke fundet i databasen");

                if (bookEpub == null)
                {
                    return;
                }
            }
            var storyboard = Storyboards.Storyboard.Main;
            var vc = (ReadBookViewController)storyboard.InstantiateViewController(ReadBookViewController.StoryboardIdentifier);
            vc.Book = this.ViewModel.Book;
            vc.EpubBook = bookEpub;
            this.NavigationController.PushViewController(vc, true);
        }

        private async void BorrowBook_Clicked()
        {
            var yes = await this.DialogService.ConfirmAsync("Er du sikker p� at du �nsker at l�ne denne bog i 30 dage?", "Godkend l�n", "OK");
            if (!yes)
            {
                return;
            }

            await this.ViewModel.BorrowBook();
        }

        protected override void CreateBindings()
        {
            this.imgBookCover.BindImageUrl(this.ViewModel, vm => vm.Book.Id);
            this.lblBookTitle.BindText(this.ViewModel, vm => vm.Book.Title);
            this.lblAuthor.BindText(this.ViewModel, vm => vm.Book.Author.Name, "af {0}");
            this.lblChapters.Bind(
                this.ViewModel,
                vm => vm.Book.PublishYear,
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