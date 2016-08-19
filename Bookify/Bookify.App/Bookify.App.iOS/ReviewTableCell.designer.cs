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
        UIKit.UIImageView imgRating1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgRating2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgRating3 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgRating4 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgRating5 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAuthor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblComment { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgRating1 != null) {
                imgRating1.Dispose ();
                imgRating1 = null;
            }

            if (imgRating2 != null) {
                imgRating2.Dispose ();
                imgRating2 = null;
            }

            if (imgRating3 != null) {
                imgRating3.Dispose ();
                imgRating3 = null;
            }

            if (imgRating4 != null) {
                imgRating4.Dispose ();
                imgRating4 = null;
            }

            if (imgRating5 != null) {
                imgRating5.Dispose ();
                imgRating5 = null;
            }

            if (lblAuthor != null) {
                lblAuthor.Dispose ();
                lblAuthor = null;
            }

            if (lblComment != null) {
                lblComment.Dispose ();
                lblComment = null;
            }
        }
    }
}