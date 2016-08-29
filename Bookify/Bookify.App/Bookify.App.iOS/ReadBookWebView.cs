using Foundation;
using System;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class ReadBookWebView : UIWebView
    {
        public ReadBookWebView(IntPtr handle) : base(handle)
        {
            this.Delegate = new ReadBookkWebViewDelegate();
        }

        public class ReadBookkWebViewDelegate : UIWebViewDelegate
        {
            public override bool ShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
            {
                if (navigationType == UIWebViewNavigationType.LinkClicked)
                {
                    var app = UIApplication.SharedApplication;
                    if (app.CanOpenUrl(request.Url))
                    {
                        app.OpenUrl(request.Url);
                    }
                    return false;
                }
                return true;
            }
        }
    }
}