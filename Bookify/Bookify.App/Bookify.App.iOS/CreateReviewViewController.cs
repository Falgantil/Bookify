using Foundation;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.App.Core.Initialization;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.General;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.Common.Models;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class CreateReviewViewController : ExtendedViewController<CreateReviewViewModel>
    {
        public const string StoryboardIdentifier = "CreateReviewViewController";

        private const int ConstraintDefaultValue = 20;

        private KeyboardNotificationManager keyboardManager;
        private UITapGestureRecognizer gestureTap1;
        private UITapGestureRecognizer gestureTap2;
        private UITapGestureRecognizer gestureTap3;
        private UITapGestureRecognizer gestureTap4;
        private UITapGestureRecognizer gestureTap5;

        public CreateReviewViewController(IntPtr handle) : base(handle)
        {
        }

        protected override CreateReviewViewModel CreateViewModel()
        {
            return AppDelegate.Root.Resolve<CreateReviewViewModel>(new Parameter("book", this.Book));
        }

        public DetailedBookDto Book { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.gestureTap1 = new UITapGestureRecognizer(() => this.ViewModel.Rating = 1);
            this.gestureTap2 = new UITapGestureRecognizer(() => this.ViewModel.Rating = 2);
            this.gestureTap3 = new UITapGestureRecognizer(() => this.ViewModel.Rating = 3);
            this.gestureTap4 = new UITapGestureRecognizer(() => this.ViewModel.Rating = 4);
            this.gestureTap5 = new UITapGestureRecognizer(() => this.ViewModel.Rating = 5);

            this.txtMessage.Placeholder = "Skriv vurdering her...";

            this.imgRating1.UserInteractionEnabled = true;
            this.imgRating2.UserInteractionEnabled = true;
            this.imgRating3.UserInteractionEnabled = true;
            this.imgRating4.UserInteractionEnabled = true;
            this.imgRating5.UserInteractionEnabled = true;
            this.imgRating1.AddGestureRecognizer(this.gestureTap1);
            this.imgRating2.AddGestureRecognizer(this.gestureTap2);
            this.imgRating3.AddGestureRecognizer(this.gestureTap3);
            this.imgRating4.AddGestureRecognizer(this.gestureTap4);
            this.imgRating5.AddGestureRecognizer(this.gestureTap5);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.keyboardManager.Dispose();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var btnCreateReview = new UIBarButtonItem(UIBarButtonSystemItem.Save, this.BtnCreateReview_Click);
            this.NavigationItem.SetRightBarButtonItem(btnCreateReview, false);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.keyboardManager = new KeyboardNotificationManager();
            this.keyboardManager.KeyboardWillShow += this.KeyboardManagerOnKeyboardWillShow;
            this.keyboardManager.KeyboardWillHide += this.KeyboardManagerOnKeyboardWillHide;
        }

        private void KeyboardManagerOnKeyboardWillShow(object sender, KeyboardNotificationManager.KeyboardNotificationEventArgs args)
        {
            UIView.Animate(
                args.Duration.TotalSeconds,
                () =>
                {
                    this.constBottomConstraint.Constant = ConstraintDefaultValue + args.KeyboardSize.Height;
                    this.View.LayoutIfNeeded();
                });
        }

        private void KeyboardManagerOnKeyboardWillHide(object sender, KeyboardNotificationManager.KeyboardNotificationEventArgs args)
        {
            UIView.Animate(
                args.Duration.TotalSeconds,
                () =>
                {
                    this.constBottomConstraint.Constant = ConstraintDefaultValue;
                    this.View.LayoutIfNeeded();
                });
        }

        private async void BtnCreateReview_Click(object sender, EventArgs e)
        {
            var errors = this.ViewModel.VerifyReview().ToArray();
            if (errors.Any())
            {
                var msg = new StringBuilder();

                foreach (var err in errors)
                {
                    msg.Append(err);
                }

                this.DialogService.Alert(msg.ToString().Trim(), "Fejl i indtastning", "Tilbage");
                return;
            }

            using (this.DialogService.Loading("Opretter anmeldelse..."))
            {
                var dto = await this.TryTask(async () => await this.ViewModel.CreateReview());
                if (dto != null)
                {
                    this.CreatedReview?.Invoke(this, dto);
                    this.NavigationController.PopViewController(true);
                }
            }
        }

        public event EventHandler<BookFeedbackDto> CreatedReview;

        protected override void CreateBindings()
        {
            this.View.BindRating(
                this.ViewModel,
                vm => vm.Rating,
                this.imgRating1,
                this.imgRating2,
                this.imgRating3,
                this.imgRating4,
                this.imgRating5);
        }
    }
}