using Foundation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class PlaceholderTextView : UITextView
    {
        private string placeholder;

        public PlaceholderTextView(IntPtr handle) : base(handle)
        {
            this.Initialize();
        }

        public string Placeholder
        {
            get { return this.placeholder; }
            set
            {
                if (this.Text == this.placeholder)
                {
                    this.Text = string.Empty;
                }
                this.placeholder = value;

                this.FinishedEditing();
            }
        }

        public Lazy<UIToolbar> Toolbar { get; private set; }

        private void Initialize()
        {
            this.ShouldBeginEditing = t =>
            {
                this.BeginEditing();

                this.InputAccessoryView = this.Toolbar.Value;

                return true;
            };

            this.ShouldEndEditing = t =>
            {
                this.FinishedEditing();

                this.InputAccessoryView = null;

                return true;
            };

            this.Toolbar = new Lazy<UIToolbar>(() =>
            {
                var parentSize = this.Superview.Frame;
                var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, (float)parentSize.Width, 44.0f));
                toolbar.SetItems(new[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) => this.ResignFirstResponder())
                }, false);
                return toolbar;
            });
        }

        public override bool ResignFirstResponder()
        {
            var resigned = base.ResignFirstResponder();
            this.LostFocus?.Invoke(this, EventArgs.Empty);
            return resigned;
        }

        public event EventHandler LostFocus;

        private void FinishedEditing()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.Placeholder;
                this.TextColor = UIColor.LightGray;
            }
        }

        private void BeginEditing()
        {
            if (this.Text == this.Placeholder)
            {
                this.Text = string.Empty;
            }

            this.TextColor = UIColor.Black;
        }
    }
}