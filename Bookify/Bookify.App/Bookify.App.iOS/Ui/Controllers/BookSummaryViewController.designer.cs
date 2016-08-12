// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    [Register ("BookSummaryViewController")]
    partial class BookSummaryViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint constImageHeight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgBookCover { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAuthor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBookTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblChapters { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblSummary { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView scrollviewContent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView viewTitleBackground { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (constImageHeight != null) {
                constImageHeight.Dispose ();
                constImageHeight = null;
            }

            if (imgBookCover != null) {
                imgBookCover.Dispose ();
                imgBookCover = null;
            }

            if (lblAuthor != null) {
                lblAuthor.Dispose ();
                lblAuthor = null;
            }

            if (lblBookTitle != null) {
                lblBookTitle.Dispose ();
                lblBookTitle = null;
            }

            if (lblChapters != null) {
                lblChapters.Dispose ();
                lblChapters = null;
            }

            if (lblSummary != null) {
                lblSummary.Dispose ();
                lblSummary = null;
            }

            if (scrollviewContent != null) {
                scrollviewContent.Dispose ();
                scrollviewContent = null;
            }

            if (viewTitleBackground != null) {
                viewTitleBackground.Dispose ();
                viewTitleBackground = null;
            }
        }
    }
}