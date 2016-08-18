using SidebarNavigation;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class FrontSidebarController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var storyboard = Storyboards.Storyboard.Main;

            var vcContent = (FrontTabBarController)storyboard.InstantiateViewController(FrontTabBarController.StoryboardIdentifier);
            var sidebarMenu = new FrontSidebarMenuController();
            this.Sidebar = new SidebarController(
                this,
                new UINavigationController(vcContent),
                new UINavigationController(sidebarMenu))
            {
                MenuLocation = SidebarController.MenuLocations.Left
            };
            vcContent.SidebarController = this.Sidebar;
            sidebarMenu.SidebarController = this.Sidebar;
            vcContent.Parent = this;
        }
        
        public SidebarController Sidebar { get; set; }
    }
}
