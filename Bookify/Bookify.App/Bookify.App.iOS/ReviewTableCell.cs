using Foundation;
using System;
using System.Collections.Generic;
using Bookify.App.Core.Models;
using Bookify.App.iOS.Ui.Helpers;
using Bookify.App.iOS.Ui.TableCells;
using Rope.Net;
using Rope.Net.iOS;
using UIKit;

namespace Bookify.App.iOS
{
    public partial class ReviewTableCell : UITableViewCell
    {
        public const string ReuseIdentifier = "ReviewTblCell";

        private ReviewModel model;

        private readonly List<IBinding> bindings = new List<IBinding>();


        public ReviewTableCell()
            : base(UITableViewCellStyle.Default, ReuseIdentifier)
        {
        }

        protected internal ReviewTableCell(IntPtr handle)
            : base(handle)
        {
        }

        public void Initialize(ReviewModel book)
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
            this.bindings.Add(this.lblDate.Bind(this.model, m => m.CreatedTs, (lbl, date) => lbl.Text = date.ToShortDateString()));
            this.bindings.Add(this.lblAuthor.BindText(this.model, m => m.Author));
            this.bindings.Add(this.lblComment.BindText(this.model, m => m.Message));
            this.bindings.Add(this.BindRating(this.model, m => m.Rating, this.imgStar1, this.imgStar2, this.imgStar3, this.imgStar4, this.imgStar5));
        }

        public static ReviewTableCell CreateCell(UITableView table, NSIndexPath index, ReviewModel book)
        {
            var cell = table.DequeueReusableCell(ReuseIdentifier, index);
            var bookCell = cell as ReviewTableCell ?? new ReviewTableCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }
}