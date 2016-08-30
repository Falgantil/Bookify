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
    [Register ("CreateReviewViewController")]
    partial class CreateReviewViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint constBottomConstraint { get; set; }

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
        UIKit.UILabel lblCreatorName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        PlaceholderTextView txtMessage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (this.constBottomConstraint != null) {
                this.constBottomConstraint.Dispose ();
                this.constBottomConstraint = null;
            }

            if (this.imgRating1 != null) {
                this.imgRating1.Dispose ();
                this.imgRating1 = null;
            }

            if (this.imgRating2 != null) {
                this.imgRating2.Dispose ();
                this.imgRating2 = null;
            }

            if (this.imgRating3 != null) {
                this.imgRating3.Dispose ();
                this.imgRating3 = null;
            }

            if (this.imgRating4 != null) {
                this.imgRating4.Dispose ();
                this.imgRating4 = null;
            }

            if (this.imgRating5 != null) {
                this.imgRating5.Dispose ();
                this.imgRating5 = null;
            }

            if (this.lblCreatorName != null) {
                this.lblCreatorName.Dispose ();
                this.lblCreatorName = null;
            }

            if (this.txtMessage != null) {
                this.txtMessage.Dispose ();
                this.txtMessage = null;
            }
        }
    }
}