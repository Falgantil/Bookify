using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;

using MonoTouch.Dialog;
using SidebarNavigation;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class FrontSidebarMenuController : ExtendedDialogViewController<SidebarViewModel>
    {
        public FrontSidebarMenuController()
            : base(UITableViewStyle.Grouped, null)
        {
            this.CreateMenuItems();
            this.ViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != nameof(this.ViewModel.Account))
                {
                    return;
                }
                this.InvokeOnMainThread(this.CreateMenuItems);
            };
        }

        public SidebarController SidebarController { get; set; }

        private void CreateMenuItems()
        {
            if (this.ViewModel.IsLoggedIn)
            {
                this.CreateLoggedInMenuItems();
            }
            else
            {
                this.CreateNotLoggedInMenuItems();
            }
        }

        private void CreateLoggedInMenuItems()
        {
            var eleBuySubscription = this.BtnBuySubscription();
            var eleShoppingCart = this.BtnShoppingCart();
            var eleLogout = this.BtnLogout();

            List<Element> elementsApp = new List<Element>();
            if (!this.ViewModel.Account.Person.IsSubscribed)
            {
                elementsApp.Add(eleBuySubscription);
            }
            elementsApp.Add(eleShoppingCart);

            var section1 = new Section();
            section1.AddAll(elementsApp);
            this.Root = new RootElement($"Velkommen {this.ViewModel.Account.Person.FirstName}")
            {
                section1,
                new Section
                {
                    eleLogout
                }
            };
        }

        private void CreateNotLoggedInMenuItems()
        {
            this.Root = new RootElement("Velkommen")
            {
                new Section
                {
                    this.BtnShoppingCart()
                },
                new Section
                {
                    this.BtnLogin(),
                    this.BtnRegister()
                }
            };
        }

        private async void BtnBuySubscription_Clicked()
        {
            await this.CloseMenu();
            var viewController = new PaymentViewController("Abonnement betaling");
            var contentAreaController = (UINavigationController)this.SidebarController.ContentAreaController;
            contentAreaController.PushViewController(viewController, true);

            viewController.PaymentCompleted += async (s, e) =>
            {
                await this.CloseMenu();
                await this.ViewModel.PurchaseSubscription();
                this.CreateMenuItems();
            };
        }

        private async void BtnLogout_Clicked()
        {
            if (!await this.DialogService.ConfirmAsync("Ønsker du at logge ud?", "Godkend", "Log ud", "Annuller"))
            {
                return;
            }
            await Task.Delay(100);
            await this.ViewModel.Logout();
        }

        private async void BtnRegister_Clicked()
        {
            await this.CloseMenu();

            var viewController = new RegistrationViewController();
            var navVc = new UINavigationController(viewController);
            viewController.Parent = this;
            await this.SidebarController.ParentViewController.PresentViewControllerAsync(navVc, true);
        }

        private async void BtnShoppingCart_Clicked()
        {
            await this.CloseMenu();

            var storyboard = Storyboards.Storyboard.Main;
            var vc = storyboard.InstantiateViewController(ShoppingCartViewController.StoryboardIdentifier);
            var vcShoppingCart = (ShoppingCartViewController)vc;
            var contentAreaController = (UINavigationController)this.SidebarController.ContentAreaController;
            contentAreaController.PushViewController(vcShoppingCart, true);
        }

        private async void BtnLogin_Clicked()
        {
            await this.CloseMenu();
            var storyboard = Storyboards.Storyboard.Main;
            var vcLogin = (LoginViewController)storyboard.InstantiateViewController(LoginViewController.StoryboardIdentifier);
            vcLogin.Parent = this;
            await this.SidebarController.ParentViewController.PresentViewControllerAsync(new UINavigationController(vcLogin), true);
        }

        private async Task CloseMenu()
        {
            this.SidebarController.CloseMenu();
            await Task.Delay(300);
        }

        #region Buttons

        private StyledStringElement BtnBuySubscription() => new StyledStringElement("Køb abonnement", this.BtnBuySubscription_Clicked)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator,
            Image = UIImage.FromBundle("Icons/Subscribe.png"),
            BackgroundColor = UIColor.White
        };

        private StyledStringElement BtnShoppingCart() => new StyledStringElement("Indkøbs kurv", this.BtnShoppingCart_Clicked)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator,
            Image = UIImage.FromBundle("Icons/Cart.png"),
            BackgroundColor = UIColor.White
        };

        private StyledStringElement BtnLogout() => new StyledStringElement("Log ud", this.BtnLogout_Clicked)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator,
            Image = UIImage.FromBundle("Icons/Logout.png"),
            BackgroundColor = UIColor.White
        };

        private StyledStringElement BtnLogin() => new StyledStringElement("Log ind", this.BtnLogin_Clicked)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator,
            Image = UIImage.FromBundle("Icons/Login.png"),
            BackgroundColor = UIColor.White
        };

        private StyledStringElement BtnRegister() => new StyledStringElement("Registrer", this.BtnRegister_Clicked)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator,
            Image = UIImage.FromBundle("Icons/Register.png"),
            BackgroundColor = UIColor.White
        };

        #endregion
    }
}
