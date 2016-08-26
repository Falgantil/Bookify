using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Controllers.Base;
using CoreGraphics;
using MonoTouch.Dialog;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Controllers
{
    public class PaymentViewController : ExtendedDialogViewController<PaymentViewModel>, IUIPickerViewDelegate
    {
        public PaymentViewController(string title) : base(UITableViewStyle.Grouped, null, true)
        {
            var eleCardNumber = new EntryElement(string.Empty, "Kort nummer", this.ViewModel.CardNumber) { KeyboardType = UIKeyboardType.NumberPad };
            var eleExpirationDate = new StringElement("Udløbs dato", this.ViewModel.ExpirationDate.ToString("MM-yy"));
            var eleSecurityCode = new EntryElement(string.Empty, "Sikkerheds kode", this.ViewModel.SecurityCode) { KeyboardType = UIKeyboardType.NumberPad };
            this.Root = new RootElement(title)
            {
                new Section
                {
                    eleCardNumber,
                    eleExpirationDate,
                    eleSecurityCode
                }
            };

            eleCardNumber.BindText(this.ViewModel, vm => vm.CardNumber);
            eleExpirationDate.BindText(this.ViewModel, vm => this.ViewModel.ExpirationDateText);
            eleSecurityCode.BindText(this.ViewModel, vm => this.ViewModel.SecurityCode);

            eleExpirationDate.Tapped += this.EleExpirationDateOnTapped;

            this.toolbar = new UIToolbar(new CGRect(0, 0, 320, 44));
            this.toolbar.SetItems(new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) => this.View.EndEditing(true)),
            }, false);
        }

        private readonly UIToolbar toolbar;
        public override UIView InputAccessoryView => this.toolbar;

        private async void EleExpirationDateOnTapped()
        {
            UIToolbar toolBar = new UIToolbar(new CGRect(0, 0, 320, 44)) { BarStyle = UIBarStyle.Default };
            var topView = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
            var picker = new UIPickerView { Model = new MonthPickerModel(this.ViewModel) };

            var pickerFrame = picker.Frame;
            picker.Frame = new CGRect(0, 0, pickerFrame.Width, pickerFrame.Height);
            pickerFrame = picker.Frame;

            var toolbarFrame = toolBar.Frame;
            var view = new UIView(new CGRect(0, 0, pickerFrame.Width, pickerFrame.Height + toolbarFrame.Height));
            UIBarButtonItem barButtonDone = new UIBarButtonItem(UIBarButtonSystemItem.Done, async (s, e) =>
            {
                await UIView.AnimateAsync(.25, () =>
                {
                    view.Frame = new CGRect(0, topView.Frame.Height, view.Frame.Width, view.Frame.Height);
                });
                view.RemoveFromSuperview();
            });
            toolBar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                barButtonDone
            };
            picker.AddSubview(toolBar);

            view.AddSubview(toolBar);

            picker.Frame = new CGRect(0, toolBar.Frame.Height, picker.Frame.Width, picker.Frame.Height);
            view.AddSubview(picker);

            topView.AddSubview(view);
            view.Frame = new CGRect(0, topView.Frame.Height, view.Frame.Width, view.Frame.Height);
            await UIView.AnimateAsync(.25, () =>
            {
                view.Frame = new CGRect(0, topView.Frame.Height - view.Frame.Height, view.Frame.Width, view.Frame.Height);
            });
            //view.Frame = new CGRect(0, topView.Frame.Height - view.Frame.Height, view.Frame.Width, view.Frame.Height);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Betal", UIBarButtonItemStyle.Plain, this.BtnPay_Click), false);
        }

        public event EventHandler PaymentCompleted;

        private async void BtnPay_Click(object sender, EventArgs e)
        {
            var errors = this.ViewModel.VerifyData().ToArray();
            if (errors.Any())
            {
                StringBuilder msg = new StringBuilder();
                foreach (var err in errors)
                {
                    msg.Append(err);
                }
                await this.DialogService.AlertAsync(msg.ToString().Trim(), "Fejl i indtastede data", "Tilbage");
                return;
            }
            using (this.DialogService.Loading("Godkender betaling..."))
            {
                await Task.Delay(2500);
            }

            this.NavigationController.PopViewController(true);
            await Task.Delay(300);

            this.PaymentCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public class MonthPickerModel : UIPickerViewModel
    {
        private readonly PaymentViewModel viewModel;

        public MonthPickerModel(PaymentViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 2;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            switch (component)
            {
                case 0:
                    return this.GetMonths();
                case 1:
                    return this.GetYears();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            var date = this.viewModel.ExpirationDate;

            if (component == 0)
            {
                this.viewModel.ExpirationDate = new DateTime(date.Year, (int)(row + 1), 1);
            }
            else if (component == 1)
            {
                this.viewModel.ExpirationDate = new DateTime((int)(DateTime.Today.Year + row), date.Month, 1);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            if (component == 0)
            {
                var thisMonth = new DateTime(2000, (int)(1 + row), 1);
                return thisMonth.ToString("MMM", CultureInfo.InstalledUICulture);
            }
            if (component == 1)
            {
                return (row + DateTime.Today.Year).ToString();
            }
            throw new ArgumentOutOfRangeException();
        }

        private int GetMonths()
        {
            return 12;
        }

        private int GetYears()
        {
            return 50;
        }
    }
}