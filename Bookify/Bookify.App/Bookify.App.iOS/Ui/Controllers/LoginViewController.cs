using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.General;
using Bookify.App.Sdk.Exceptions;
using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class LoginViewController : ExtendedViewController<LoginViewModel>
    {
        public const string StoryboardIdentifier = "LoginViewController";

        private const int ConstraintDefaultValue = 44;

        private bool shouldHide;

        private KeyboardNotificationManager keyboardManager;

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public FrontSidebarMenuController Parent { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.txtEmail.ShouldReturn = field =>
            {
                this.txtPassword.BecomeFirstResponder();
                return true;
            };
            this.txtPassword.ShouldReturn = field =>
            {
                this.txtPassword.ResignFirstResponder();
                return true;
            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var btnBack = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, this.BtnBack_Clicked);
            this.NavigationItem.SetLeftBarButtonItem(btnBack, false);
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await this.DismissLoginView();
        }

        protected override void CreateBindings()
        {
            this.txtEmail.BindText(this.ViewModel, vm => vm.Email);
            this.txtPassword.BindText(this.ViewModel, vm => vm.Password);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.keyboardManager = new KeyboardNotificationManager();
            this.keyboardManager.KeyboardWillShow += this.KeyboardManagerOnKeyboardWillShow;
            this.keyboardManager.KeyboardWillHide += this.KeyboardManagerOnKeyboardWillHide;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            this.keyboardManager.Dispose();
        }

        private void KeyboardManagerOnKeyboardWillHide(object sender, KeyboardNotificationManager.KeyboardNotificationEventArgs args)
        {
            UIView.Animate(
                args.Duration.TotalSeconds,
                () =>
                {
                    this.constBottomConstraint.Constant = ConstraintDefaultValue;
                    if (this.shouldHide)
                    {
                        this.imgLogo.Alpha = 1;
                    }
                    this.View.LayoutIfNeeded();
                });
        }

        private void KeyboardManagerOnKeyboardWillShow(object sender, KeyboardNotificationManager.KeyboardNotificationEventArgs args)
        {
            this.shouldHide = this.txtEmail.Frame.Top - args.KeyboardSize.Height < this.imgLogo.Frame.Bottom;

            UIView.Animate(
                args.Duration.TotalSeconds,
                () =>
                {
                    this.constBottomConstraint.Constant = ConstraintDefaultValue + args.KeyboardSize.Height;
                    if (this.shouldHide)
                    {
                        this.imgLogo.Alpha = 0;
                    }
                    this.View.LayoutIfNeeded();
                });
        }

        async partial void BtnLogin_TouchUpInside(UIButton sender)
        {
            using (this.DialogService.Loading("Logger ind, vent venligst..."))
            {
                try
                {
                    await this.ViewModel.Authenticate();
                    await this.DismissLoginView();
                }
                catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    this.DialogService.Alert("Dit brugernavn eller kodeord kunne ikke genkendes.", "Login fejl", "OK");
                }
                catch (WebException ex)
                {
                    this.DialogService.Alert("Der opstod en fejl under login. Check din internet forbindelse, eller prøv igen senere.", "Forbindelses fejl", "OK");
                }
            }
        }

        private async Task DismissLoginView()
        {
            await this.Parent.SidebarController.ParentViewController.DismissViewControllerAsync(true);
            //this.Parent.SidebarController.OpenMenu();
        }
    }
}