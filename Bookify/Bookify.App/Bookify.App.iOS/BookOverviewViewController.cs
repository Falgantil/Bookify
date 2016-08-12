using Foundation;
using System;
using Bookify.App.Core.Models;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class BookOverviewViewController : UITabBarController
    {
        public BookOverviewViewController (IntPtr handle) : base (handle)
        {
        }

        public BookModel Book { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            foreach (var vc in this.ViewControllers)
            {
                var bookSummaryVc = vc as BookSummaryViewController;
                if (bookSummaryVc != null)
                {
                    bookSummaryVc.Book = this.Book;
                }
                var reviewsVc = vc as ReviewsViewController;
                if (reviewsVc != null)
                {
                    reviewsVc.Book = this.Book;
                }
            }
        }
    }
}