using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;

namespace Bookify.App.iOS
{
    public partial class BrowseViewController : ExtendedViewController<BrowseViewModel>
    {
        public const string StoryboardIdentifier = "BrowseViewController";

        public BrowseViewController(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateBindings()
        {

        }
    }
}