using System;
using System.Drawing;

using Foundation;

using UIKit;

namespace Bookify.App.iOS.Ui.General
{
    public class KeyboardNotificationManager : IDisposable
    {
        private readonly NSObject obsWillShow;
        private readonly NSObject obsWillHide;

        private bool isShowing;

        public KeyboardNotificationManager()
        {
            this.obsWillShow = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, this.WillShow);
            this.obsWillHide = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, this.WillHide);
        }

        private void WillShow(NSNotification notification)
        {
            if (this.isShowing)
            {
                return;
            }

            var duration = UIKeyboard.AnimationDurationFromNotification(notification);
            var keyboardBounds = ((NSValue)notification.UserInfo.ObjectForKey(UIKeyboard.BoundsUserInfoKey)).RectangleFValue;

            this.KeyboardWillShow?.Invoke(this, new KeyboardNotificationEventArgs(TimeSpan.FromSeconds(duration), keyboardBounds));

            this.isShowing = true;
        }

        private void WillHide(NSNotification notification)
        {
            var duration = UIKeyboard.AnimationDurationFromNotification(notification);

            this.KeyboardWillHide?.Invoke(this, new KeyboardNotificationEventArgs(TimeSpan.FromSeconds(duration), RectangleF.Empty));

            this.isShowing = false;
        }

        public event EventHandler<KeyboardNotificationEventArgs> KeyboardWillShow;
        public event EventHandler<KeyboardNotificationEventArgs> KeyboardWillHide;

        public class KeyboardNotificationEventArgs
        {
            public TimeSpan Duration { get; set; }

            public RectangleF KeyboardSize { get; set; }

            public KeyboardNotificationEventArgs(TimeSpan duration, RectangleF keyboardSize)
            {
                this.Duration = duration;
                this.KeyboardSize = keyboardSize;
            }
        }

        public void Dispose()
        {
            NSNotificationCenter.DefaultCenter.RemoveObservers(new[] { this.obsWillShow, this.obsWillHide });
        }
    }
}
