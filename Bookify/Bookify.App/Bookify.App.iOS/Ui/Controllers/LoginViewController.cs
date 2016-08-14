using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Bookify.App.Core.Exceptions;
using Bookify.App.Core.Helpers;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.iOS.Ui.General;
using CoreAnimation;
using CoreGraphics;
using Foundation;

using Polly;

using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class LoginViewController : ExtendedViewController<LoginViewModel>
    {
        private bool shouldHide;

        private KeyboardNotificationManager keyboardManager;

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.txtUsername.ShouldReturn = field =>
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

        protected override void CreateBindings()
        {
            this.txtUsername.BindText(this.ViewModel, vm => vm.Username);
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
                    this.constBottomConstraint.Constant = 64;
                    if (this.shouldHide)
                    {
                        this.imgLogo.Alpha = 1;
                    }
                    this.View.LayoutIfNeeded();
                });
        }

        private void KeyboardManagerOnKeyboardWillShow(object sender, KeyboardNotificationManager.KeyboardNotificationEventArgs args)
        {
            this.shouldHide = this.txtUsername.Frame.Top - args.KeyboardSize.Height < this.imgLogo.Frame.Bottom;

            UIView.Animate(
                args.Duration.TotalSeconds,
                () =>
                {
                    this.constBottomConstraint.Constant = 64 + args.KeyboardSize.Height;
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
                    var account = await this.ViewModel.Authenticate();
                    var viewController = new FrontSidebarController();
                    await this.PresentViewControllerAsync(viewController, true);
                    UIApplication.SharedApplication.KeyWindow.RootViewController = viewController;
                }
                catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
                {
                    this.DialogService.Alert("Dit brugernavn eller kodeord kunne ikke genkendes.", "Login fejl", "OK");
                }
                catch (WebException ex)
                {
                    this.DialogService.Alert("Der opstod en fejl under login. Check din internet forbindelse, eller prøv igen senere.", "Forbindelses fejl", "OK");
                }
            }
        }
    }
}