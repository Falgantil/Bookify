using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Initialization;
using Bookify.App.iOS.Ui.Controllers.Base;
using Bookify.App.Sdk.Exceptions;
using MonoTouch.Dialog;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class RegistrationViewController : ExtendedDialogViewController<RegistrationViewModel>
    {
        public RegistrationViewController()
            : base(UITableViewStyle.Grouped, null, true)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var eleFirstName = new EntryElement(string.Empty, "Fornavn", this.ViewModel.FirstName)
            {
                AutocapitalizationType = UITextAutocapitalizationType.Words,
                AutocorrectionType = UITextAutocorrectionType.No
            };
            var eleLastName = new EntryElement(string.Empty, "Efternavn", this.ViewModel.LastName)
            {
                AutocapitalizationType = UITextAutocapitalizationType.Words,
                AutocorrectionType = UITextAutocorrectionType.No
            };

            var eleEmail = new EntryElement(string.Empty, "Email", this.ViewModel.Email)
            {
                KeyboardType = UIKeyboardType.EmailAddress,
                AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None
            };
            var elePassword = new EntryElement(string.Empty, "Kodeord", this.ViewModel.Password, true);
            var elePasswordRepeat = new EntryElement(string.Empty, "Gentag kodeord", this.ViewModel.PasswordRepeat, true);
            var eleUsername = new EntryElement(string.Empty, "Brugernavn", this.ViewModel.Username)
            {
                AutocorrectionType = UITextAutocorrectionType.No,
                AutocapitalizationType = UITextAutocapitalizationType.None
            };

            this.Root = new RootElement("Registrer")
            {
                new Section("Personlige oplysninger")
                {
                    eleFirstName,
                    eleLastName,
                },
                new Section("Konto oplysninger")
                {
                    eleEmail,
                    elePassword,
                    elePasswordRepeat,
                    eleUsername
                }
            };

            eleFirstName.BindText(this.ViewModel, vm => vm.FirstName);
            eleLastName.BindText(this.ViewModel, vm => vm.LastName);

            eleEmail.BindText(this.ViewModel, vm => vm.Email);
            elePassword.BindText(this.ViewModel, vm => vm.Password);
            elePasswordRepeat.BindText(this.ViewModel, vm => vm.PasswordRepeat);
            eleUsername.BindText(this.ViewModel, vm => vm.Username);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Cancel, this.BtnCancel_Clicked), false);
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Register", UIBarButtonItemStyle.Plain, this.BtnRegister_Clicked), false);
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            var fields = this.ViewModel.GetInvalidFields().ToArray();
            if (fields.Length > 0)
            {
                StringBuilder message = new StringBuilder();
                foreach (var field in fields)
                {
                    if (field == nameof(this.ViewModel.FirstName)) message.Append("Indtast fornavn. ");
                    if (field == nameof(this.ViewModel.LastName)) message.Append("Indtast efternavn. ");
                    if (field == nameof(this.ViewModel.Email)) message.Append("Indtast email adresse. ");
                    if (field == nameof(this.ViewModel.Password)) message.Append("Indtast kodeord. ");
                    if (field == nameof(this.ViewModel.PasswordRepeat)) message.Append("Gentag kodeord. ");
                    if (field == nameof(this.ViewModel.Username)) message.Append("Indtast brugernavn. ");
                    if (field == $"{nameof(this.ViewModel.Password)} - {nameof(this.ViewModel.PasswordRepeat)}") message.Append("Kodeorderene passer ikke. ");
                }
                var msg = message.ToString().Trim();
                await this.DialogService.AlertAsync(msg, "Fejl i indtastede data", "Tilbage");
                return;
            }
            try
            {
                await this.ViewModel.Register();
                this.DialogService.Toast("Success!", TimeSpan.FromSeconds(3));
                this.DismissView();
            }
            catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                await this.DialogService.AlertAsync("Der findes allerede en bruger med den angivne email adresse!", "Fejl");
            }
            catch (HttpResponseException)
            {
                await this.DialogService.AlertAsync("Der opstod en fejl på serveren, og din bruger kunne ikke oprettes. Prøv venligst igen senere", "Fejl");
            }
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            this.DismissView();
        }

        private async void DismissView()
        {
            await this.Parent.SidebarController.ParentViewController.DismissViewControllerAsync(true);
        }
        
        public FrontSidebarMenuController Parent { get; set; }
    }
}