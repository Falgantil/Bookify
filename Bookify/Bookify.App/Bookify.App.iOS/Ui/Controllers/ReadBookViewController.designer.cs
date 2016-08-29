// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;

using Bookify.App.iOS.Ui.Controls;

using Foundation;

namespace Bookify.App.iOS.Ui.Controllers
{
    [Register ("ReadBookViewController")]
    partial class ReadBookViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        ReadBookWebView webContent { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (this.webContent != null) {
                this.webContent.Dispose ();
                this.webContent = null;
            }
        }
    }
}