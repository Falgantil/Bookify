using System;
using System.Collections.Generic;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.App.iOS.Ui.TableCells;
using Bookify.Common.Models;
using Foundation;
using Rope.Net;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS.Ui.Views
{
    public partial class BookTableCell : ExtendedTableViewCell<BookDtoViewModel>
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

        public BookTableCell() : base(Key)
        {
        }

        protected override IEnumerable<IBinding> Register()
        {
            yield return this.lblBookTitle.BindText(this.Model, m => m.Book.Title);
            yield return this.imgThumbnail.BindImageUrl(this.Model, m => m.Book.Id);
        }

        public static BookTableCell CreateCell(UITableView table, NSIndexPath index, BookDto book)
        {
            return CreateCell<BookTableCell, BookDtoViewModel>(Key, table, index, new BookDtoViewModel(book));
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
