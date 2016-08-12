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

namespace Bookify.App.iOS
{
    [Register ("ReviewTableCell")]
    partial class ReviewTableCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgStar1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgStar2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgStar3 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgStar4 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgStar5 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAuthor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblComment { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDate { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgStar1 != null) {
                imgStar1.Dispose ();
                imgStar1 = null;
            }

            if (imgStar2 != null) {
                imgStar2.Dispose ();
                imgStar2 = null;
            }

            if (imgStar3 != null) {
                imgStar3.Dispose ();
                imgStar3 = null;
            }

            if (imgStar4 != null) {
                imgStar4.Dispose ();
                imgStar4 = null;
            }

            if (imgStar5 != null) {
                imgStar5.Dispose ();
                imgStar5 = null;
            }

            if (lblAuthor != null) {
                lblAuthor.Dispose ();
                lblAuthor = null;
            }

            if (lblComment != null) {
                lblComment.Dispose ();
                lblComment = null;
            }

            if (lblDate != null) {
                lblDate.Dispose ();
                lblDate = null;
            }
        }
    }
}