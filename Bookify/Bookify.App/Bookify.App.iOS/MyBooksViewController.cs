using Foundation;
using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class MyBooksViewController : ExtendedViewController<MyBooksViewModel>
    {
        public const string StoryboardIdentifier = "MyBooksViewController";

        public MyBooksViewController (IntPtr handle) : base (handle)
        {
        }

        protected override void CreateBindings()
        {
            
        }
    }
}