using System;
using System.Collections.Generic;
using System.Text;

using MonoTouch.Dialog;

using SidebarNavigation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class FrontSidebarController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var storyboard = UIStoryboard.FromName("Main", null);

            var vc = (FrontTabBarController)storyboard.InstantiateViewController(FrontTabBarController.StoryboardIdentifier);
            this.Sidebar = new SidebarController(
                this,
                new UINavigationController(vc),
                new UINavigationController(new DialogViewController(UITableViewStyle.Grouped, new RootElement("Side menu"))))
            {
                MenuLocation = SidebarController.MenuLocations.Left
            };
            vc.SidebarController = this.Sidebar;
            vc.Parent = this;
        }
        
        public SidebarController Sidebar { get; set; }
    }
}
