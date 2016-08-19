using System;
using System.Collections.Generic;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.Common.Models;
using Foundation;
using Rope.Net;
using Rope.Net.iOS;
using UIKit;

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

        private BookDtoViewModel model;

        private readonly List<IBinding> bindings = new List<IBinding>();
        
        public void Initialize(BookDto book)
        {
            this.Unregister();
            this.model = new BookDtoViewModel(book);
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
            this.bindings.Add(this.lblBookTitle.BindText(this.model, m => m.Book.Title));
            this.bindings.Add(this.imgThumbnail.BindImageUrl(this.model, m => m.Book.Id));
        }

        public static BookTableCell CreateCell(UITableView table, NSIndexPath index, BookDto book)
        {
            var cell = table.DequeueReusableCell(Key, index);
            var bookCell = cell as BookTableCell ?? new BookTableCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }

    public class BookDtoViewModel : BaseViewModel
    {
        public BookDtoViewModel(BookDto book)
        {
            this.Book = book;
        }

        public BookDto Book { get; }
    }
}
