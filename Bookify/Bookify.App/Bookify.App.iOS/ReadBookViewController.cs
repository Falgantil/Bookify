using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.Common.Models;

namespace Bookify.App.iOS
{
    public partial class ReadBookViewController : ExtendedViewController<ReadBookViewModel>
    {
        public const string StoryboardIdentifier = "ReadBookViewController";

        public ReadBookViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            using (this.DialogService.Loading("Indlæser bog..."))
            {
                this.webContent.LoadHtmlString(this.Epub, null);
            }
        }

        public BookDto Book { get; set; }

        public string Epub { get; set; }

        protected override void CreateBindings()
        {

        }
    }
}