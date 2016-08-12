using System;
using System.Linq;

using SidebarNavigation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public partial class FrontTabBarController : UITabBarController
    {
        private UIBarButtonItem btnToggleMenu;

        public const string StoryboardIdentifier = "FrontTabBarController";

        public FrontTabBarController(IntPtr handle) : base(handle)
        {
        }

        public SidebarController SidebarController { get; set; }

        public FrontSidebarController Parent { get; set; }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationItem.SetHidesBackButton(true, false);
            this.Title = this.SelectedViewController.Title;

            var iconMenu = UIImage.FromBundle("Icons/Menu.png");
            this.btnToggleMenu = new UIBarButtonItem(iconMenu, UIBarButtonItemStyle.Plain, this.MenuToggleClicked);
            this.NavigationItem.SetLeftBarButtonItem(this.btnToggleMenu, false);
        }

        private void MenuToggleClicked(object sender, EventArgs e)
        {
            this.SidebarController.ToggleMenu();
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