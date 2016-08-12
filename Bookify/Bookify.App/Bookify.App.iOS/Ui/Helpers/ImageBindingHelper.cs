using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Text;

using Akavache;

using Rope.Net;
using Rope.Net.iOS;

using Splat;

using UIKit;

namespace Bookify.App.iOS.Ui.Helpers
{
    public static class ImageBindingHelper
    {
        /// <summary>
        /// Binds the image URL to the specified image view.
        /// Now, every time the URL changes, the corrosponding image will be downloaded
        /// and applied, but ONLY if the binding has not been disposed since the
        /// download was initialized. Trust me when I say, this will fix (or avoid, I
        /// suppose) a lot of headaches you didn't know you were going to get.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="img">The img.</param>
        /// <param name="model">The model.</param>
        /// <param name="getVal">The get value.</param>
        /// <returns></returns>
        public static IBinding BindImageUrl<TModel>(this UIImageView img, TModel model, Expression<Func<TModel, string>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var disposed = false;
            BlobCache.EnsureInitialized();
            var binding = (Binding)img.Bind(
                model,
                getVal,
                async (i, url) =>
                    {
                        var image = await BlobCache.UserAccount.LoadImageFromUrl(
                        url,
                        url,
                        false,
                        null,
                        null,
                        DateTimeOffset.Now.AddDays(7));
                    if (disposed)
                    {
                        return;
                    }
                    img.Image = image.ToNative();
                });
            binding.With(() => disposed = true);
            return binding;
        }
    }
}
