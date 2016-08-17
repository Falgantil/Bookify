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

namespace Bookify.App.iOS.Ui.TableCells
{
    [Register ("CartItemTableCell")]
    partial class CartItemTableCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgThumbnail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBooksQuantity { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblBookTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgThumbnail != null) {
                imgThumbnail.Dispose ();
                imgThumbnail = null;
            }

            if (lblBooksQuantity != null) {
                lblBooksQuantity.Dispose ();
                lblBooksQuantity = null;
            }

            if (lblBookTitle != null) {
                lblBookTitle.Dispose ();
                lblBookTitle = null;
            }
        }
    }
}