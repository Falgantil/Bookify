using System;
using System.Collections.Generic;
using System.Text;

using Bookify.App.Core.Helpers;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;

using MonoTouch.Dialog;

using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class FrontSidebarMenuController : DialogViewController
    {
        public FrontSidebarMenuController()
            : base(UITableViewStyle.Grouped, null)
        {
            this.ViewModel = AppDelegate.Root.Resolve<SidebarViewModel>();
            this.CreateMenuItems();
        }

        public SidebarViewModel ViewModel { get; private set; }

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
            this.Root = new RootElement($"Velkommen {this.ViewModel.Account.Firstname}")
            {
                new Section
                {
                    new StringElement("Køb abonnement", this.BtnBuySubscription_Clicked),
                    new StringElement("Indkøbs kurv", this.BtnShoppingCart_Clicked),
                    new StringElement("Log ud", this.BtnLogout_Clicked),
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
                    new StringElement("Indkøbs kurv", this.BtnShoppingCart_Clicked),
                    new StringElement("Log ind", this.BtnLogin_Clicked),
                    new StringElement("Registrer", this.BtnRegister_Clicked)
                }
            };
        }

        private void BtnLogout_Clicked()
        {

        }

        private void BtnRegister_Clicked()
        {

        }

        private void BtnShoppingCart_Clicked()
        {

        }

        private void BtnLogin_Clicked()
        {

        }
    }
}
