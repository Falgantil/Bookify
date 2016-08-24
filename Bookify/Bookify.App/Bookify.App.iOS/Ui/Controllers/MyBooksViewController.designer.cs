// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using System.CodeDom.Compiler;
using Foundation;

namespace Bookify.App.iOS.Ui.Controllers
{
    [Register ("MyBooksViewController")]
    partial class MyBooksViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tblContent { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (this.tblContent != null) {
                this.tblContent.Dispose ();
                this.tblContent = null;
            }
        }
    }
}