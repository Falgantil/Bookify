using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Reactive.Linq;
using Akavache;
using Bookify.App.Sdk;
using Rope.Net;
using Rope.Net.iOS;

using Splat;

using UIKit;

namespace Bookify.App.iOS.Ui.Helpers
{
    public static class BindingHelper
    {
        private static readonly Lazy<UIImage> imgRating = new Lazy<UIImage>(() => UIImage.FromBundle("Icons/Heart.png"));
        private static readonly Lazy<UIImage> imgRatingFilled = new Lazy<UIImage>(() => UIImage.FromBundle("Icons/HeartFilled.png"));

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
        public static IBinding BindImageUrl<TModel>(this UIImageView img, TModel model, Expression<Func<TModel, int>> getVal)
            where TModel : INotifyPropertyChanged
        {
            var disposed = false;
            BlobCache.EnsureInitialized();
            var binding = (Binding)img.Bind(
                model,
                getVal,
                async (i, bookId) =>
                {
                    var url = AppConfig.GetThumbnail(bookId, (int)img.Frame.Width, (int)img.Frame.Height);

                    UIImage uiImage;
                    try
                    {
                        var image = await BlobCache.UserAccount.LoadImageFromUrl(
                           url,
                           url,
                           false,
                           null,
                           null,
                           DateTimeOffset.Now.AddDays(7));
                        uiImage = image.ToNative();
                    }
                    catch (WebException)
                    {
                        uiImage = UIImage.FromBundle("Icons/UnknownImage.png");
                    }
                    if (disposed)
                    {
                        return;
                    }

                    img.Image = uiImage;
                });
            binding.With(() => disposed = true);
            return binding;
        }

        public static IBinding BindRating<TModel>(this UIView parent, TModel model, Expression<Func<TModel, int>> getVal, UIImageView rating1, UIImageView rating2, UIImageView rating3, UIImageView rating4, UIImageView rating5)
            where TModel : INotifyPropertyChanged
        {
            return parent.Bind(
                model,
                getVal,
                (v, rating) =>
                    {
                        rating1.Image = rating > 0 ? imgRatingFilled.Value : imgRating.Value;
                        rating2.Image = rating > 1 ? imgRatingFilled.Value : imgRating.Value;
                        rating3.Image = rating > 2 ? imgRatingFilled.Value : imgRating.Value;
                        rating4.Image = rating > 3 ? imgRatingFilled.Value : imgRating.Value;
                        rating5.Image = rating > 4 ? imgRatingFilled.Value : imgRating.Value;
                    });
        }
    }
}
