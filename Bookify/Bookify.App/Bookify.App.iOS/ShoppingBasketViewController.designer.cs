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
    [Register ("ShoppingBasketViewController")]
    partial class ShoppingBasketViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblContent { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tblContent != null) {
                tblContent.Dispose ();
                tblContent = null;
            }
        }
    }
}