using System;
using System.Collections.Generic;
using Bookify.App.Core.Models;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.Models;
using Foundation;
using ObjCRuntime;
using Rope.Net;
using Rope.Net.iOS;
using UIKit;
using Book = Bookify.Models.Book;

namespace Bookify.App.iOS.Ui.Views
{
    public partial class BookTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("BookTableCell");
        public static readonly UINib Nib;

        static BookTableCell()
        {
            Nib = UINib.FromName("BookTableCell", NSBundle.MainBundle);
        }

        public BookTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public BookTableCell() : base(UITableViewCellStyle.Default, Key)
        {
        }

        private Book model;

        private readonly List<IBinding> bindings = new List<IBinding>();
        
        public void Initialize(Book book)
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
            this.bindings.Add(this.imgThumbnail.BindImageUrl(this.model, m => m.Id));
        }

        public static BookTableCell CreateCell(UITableView table, NSIndexPath index, Book book)
        {
            var cell = table.DequeueReusableCell(Key, index);
            var bookCell = cell as BookTableCell ?? new BookTableCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }
}
