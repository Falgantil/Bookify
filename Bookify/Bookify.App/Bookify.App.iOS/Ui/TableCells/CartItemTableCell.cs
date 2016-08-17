using System;
using System.Collections.Generic;
using Bookify.App.Core.Models;
using Bookify.App.iOS.Ui.Helpers;
using CoreGraphics;
using Foundation;
using Rope.Net;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.TableCells
{
    public partial class CartItemTableCell : UITableViewCell
    {
        public const string ReuseIdentifier = "CartItemCell";

        private CartItemModel model;

        private readonly List<IBinding> bindings = new List<IBinding>();

        public CartItemTableCell()
            : base(UITableViewCellStyle.Default, ReuseIdentifier)
        {
        }

        public CartItemTableCell (IntPtr handle) : base (handle)
        {
        }

        public void Initialize(CartItemModel cartItemModel)
        {
            this.Unregister();
            this.model = cartItemModel;
            this.Register();

            this.DisableGestureRecognizers(this.ContentView);
        }

        private void DisableGestureRecognizers(UIView view)
        {
            var recognizers = view.GestureRecognizers ?? new UIGestureRecognizer[0];
            foreach (var recognizer in recognizers)
            {
                recognizer.Enabled = false;
            }

            var subviews = view.Subviews ?? new UIView[0];
            foreach (var subview in subviews)
            {
                this.DisableGestureRecognizers(subview);
            }
        }

        private void Unregister()
        {
            if (this.model == null)
            {
                return;
            }
            this.bindings.ForEach(b => b.Dispose());
            this.bindings.Clear();
        }

        private void Register()
        {
            this.bindings.Add(this.lblBookTitle.BindText(this.model, m => m.Book.Title));
            this.bindings.Add(this.imgThumbnail.BindImageUrl(this.model, m => m.Book.Id));

            bool initialRender = true;
            this.bindings.Add(this.lblBooksQuantity.Bind(this.model, m => m.Quantity, async (lbl, count) =>
                {
                    lbl.Text = $"{count} stk.";
                    if (initialRender)
                    {
                        initialRender = false;
                        return;
                    }
                    await AnimateAsync(.1, () => lbl.Transform = CGAffineTransform.MakeScale(1.3f, 1.3f));
                    await AnimateAsync(.1, () => lbl.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f));
                }));
        }

        public static CartItemTableCell CreateCell(UITableView table, NSIndexPath index, CartItemModel book)
        {
            var cell = table.DequeueReusableCell(ReuseIdentifier, index);
            var bookCell = cell as CartItemTableCell ?? new CartItemTableCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }
}