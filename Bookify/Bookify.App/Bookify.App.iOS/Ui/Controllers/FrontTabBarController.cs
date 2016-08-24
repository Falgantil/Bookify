using System;
using System.Collections.Generic;
using System.Linq;
using Bookify.App.Core.Interfaces.Services;
using Bookify.App.Core.Services;
using Bookify.App.iOS.Initialization;
using SidebarNavigation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class FrontTabBarController : UITabBarController
    {
        private UIBarButtonItem btnToggleMenu;
        private readonly IAuthenticationService authService;

        public const string StoryboardIdentifier = "FrontTabBarController";

        public FrontTabBarController(IntPtr handle) : base(handle)
        {
            this.authService = AppDelegate.Root.Resolve<IAuthenticationService>();
        }

        public SidebarController SidebarController { get; set; }

        public FrontSidebarController Parent { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.AuthChanged(this, this.authService.LoggedOnAccount);
            this.authService.AuthChanged += this.AuthChanged;
        }

        protected override void Dispose(bool disposing)
        {
            this.authService.AuthChanged -= this.AuthChanged;
            base.Dispose(disposing);
        }

        private void AuthChanged(object sender, AccountModel account)
        {
            if (account == null)
                this.RemoveMyBooks();
            else
                this.AddMyBooks();
        }

        private void AddMyBooks()
        {
            var items = this.ViewControllers;
            var myBooksVc = items.FirstOrDefault(vc => vc is MyBooksViewController) as MyBooksViewController;
            if (myBooksVc != null)
            {
                return;
            }

            var storyboard = Storyboards.Storyboard.Main;
            var viewControllers = new List<UIViewController>(items)
            {
                storyboard.InstantiateViewController(MyBooksViewController.StoryboardIdentifier)
            };
            this.ViewControllers = viewControllers.ToArray();
        }

        private void RemoveMyBooks()
        {
            var items = this.ViewControllers;
            var myBooksVc = items.FirstOrDefault(vc => vc is MyBooksViewController) as MyBooksViewController;
            if (myBooksVc == null)
            {
                return;
            }

            this.ViewControllers = items.Where(vc => !(vc is MyBooksViewController)).ToArray();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationItem.SetHidesBackButton(true, false);
            this.Title = this.SelectedViewController.Title;

            var iconMenu = UIImage.FromBundle("Icons/Menu.png");
            this.btnToggleMenu = new UIBarButtonItem(iconMenu, UIBarButtonItemStyle.Plain, (sender, e) => this.SidebarController.ToggleMenu());
            this.NavigationItem.SetLeftBarButtonItem(this.btnToggleMenu, false);
        }

        public override void ItemSelected(UITabBar tabbar, UITabBarItem item)
        {
            var selectedVc = this.ViewControllers.FirstOrDefault(vc => vc.TabBarItem == item);
            if (selectedVc == null)
            {
                return;
            }
            this.Title = selectedVc.Title;
        }
    }
}