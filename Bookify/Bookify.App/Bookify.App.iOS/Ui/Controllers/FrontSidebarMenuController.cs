using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;

using MonoTouch.Dialog;
using SidebarNavigation;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class FrontSidebarMenuController : ExtendedDialogViewController
    {
        public FrontSidebarMenuController()
            : base(UITableViewStyle.Grouped, null)
        {
            this.ViewModel = AppDelegate.Root.Resolve<SidebarViewModel>();
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

        public SidebarViewModel ViewModel { get; private set; }
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
            this.Root = new RootElement($"Velkommen {this.ViewModel.Account.Person.FirstName}")
            {
                new Section
                {
                    new StyledStringElement("Køb abonnement", this.BtnBuySubscription_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Subscribe.png"),
                        BackgroundColor = UIColor.White
                    },
                    new StyledStringElement("Indkøbs kurv", this.BtnShoppingCart_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Cart.png"),
                        BackgroundColor = UIColor.White
                    }
                },
                new Section
                {
                    new StyledStringElement("Log ud", this.BtnLogout_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Logout.png"),
                        BackgroundColor = UIColor.White
                    }
                }
            };
        }

        private void BtnBuySubscription_Clicked()
        {

        }

        private void CreateNotLoggedInMenuItems()
        {
            this.Root = new RootElement("Velkommen")
            {
                new Section
                {
                    new StyledStringElement("Indkøbs kurv", this.BtnShoppingCart_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Cart.png"),
                        BackgroundColor = UIColor.White
                    }
                },
                new Section
                {
                    new StyledStringElement("Log ind", this.BtnLogin_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Login.png"),
                        BackgroundColor = UIColor.White

                    },
                    new StyledStringElement("Registrer", this.BtnRegister_Clicked)
                    {
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        Image = UIImage.FromBundle("Icons/Register.png"),
                        BackgroundColor = UIColor.White
                    }
                }
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

        private async Task CloseMenu()
        {
            this.SidebarController.CloseMenu();
            await Task.Delay(300);
        }

        private async void BtnLogin_Clicked()
        {
            await this.CloseMenu();
            var storyboard = Storyboards.Storyboard.Main;
            var vcLogin = (LoginViewController)storyboard.InstantiateViewController(LoginViewController.StoryboardIdentifier);
            vcLogin.Parent = this;
            await this.SidebarController.ParentViewController.PresentViewControllerAsync(new UINavigationController(vcLogin), true);
        }
    }
}
