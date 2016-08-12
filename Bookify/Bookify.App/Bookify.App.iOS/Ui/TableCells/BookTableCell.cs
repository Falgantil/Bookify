using System;
using System.Collections.Generic;

using Bookify.App.Core.Models;
using Bookify.App.iOS.Ui.Helpers;

using Foundation;

using Rope.Net;
using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.TableCells
{
    public partial class BookTableCell : UITableViewCell
    {
        public const string ReuseIdentifier = "BookTblCell";

        private LightBookModel model;

        private readonly List<IBinding> bindings = new List<IBinding>();

        public BookTableCell()
            : base(UITableViewCellStyle.Default, ReuseIdentifier)
        {
        }

        protected internal BookTableCell(IntPtr handle)
            : base(handle)
        {
        }

        public void Initialize(LightBookModel book)
        {
            this.Unregister();
            this.model = book;
            this.Register();
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
            this.bindings.Add(this.lblBookTitle.BindText(this.model, m => m.Title));
            this.bindings.Add(this.imgThumbnail.BindImageUrl(this.model, m => m.ThumbnailUrl));
        }

        public static BookTableCell CreateCell(UITableView table, NSIndexPath index, LightBookModel book)
        {
            var cell = table.DequeueReusableCell(ReuseIdentifier, index);
            var bookCell = cell as BookTableCell ?? new BookTableCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }
}