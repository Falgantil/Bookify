using System;
using System.Linq;
using Bookify.App.Core.Models;
using Bookify.Models;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class BookOverviewViewController : UITabBarController
    {
        public BookOverviewViewController (IntPtr handle) : base (handle)
        {
        }

        public Book Book { get; set; }

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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.Title = this.SelectedViewController?.Title;
        }

        public override void ItemSelected(UITabBar tabbar, UITabBarItem item)
        {
            var selectedVc = this.ViewControllers.FirstOrDefault(vc => vc.TabBarItem == item);
            if (selectedVc == null)
            {
                return;
            }
            this.Title = selectedVc.Title;
        }
    }
}