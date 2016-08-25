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
    [Register ("ReadBookViewController")]
    partial class ReadBookViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView webContent { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (webContent != null) {
                webContent.Dispose ();
                webContent = null;
            }
        }
    }
}