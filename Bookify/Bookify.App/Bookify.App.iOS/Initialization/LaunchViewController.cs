using Cirrious.FluentLayouts.Touch;

using Foundation;

using ObjCRuntime;

using UIKit;

namespace Bookify.App.iOS.Initialization
{
    public class LaunchViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var arr = NSBundle.MainBundle.LoadNib("LaunchScreen", null, null);
            var view = Runtime.GetNSObject<UIView>(arr.ValueAt(0));

            this.View.AddSubview(view);

            this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.View.AddConstraints(
                view.AtLeftOf(this.View),
                view.AtRightOf(this.View),
                view.AtTopOf(this.View),
                view.AtBottomOf(this.View));
        }
    }
}