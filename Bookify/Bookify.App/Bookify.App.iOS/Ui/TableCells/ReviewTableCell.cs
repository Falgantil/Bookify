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
    public partial class ReviewTableCell : ExtendedTableViewCell<ReviewModel>
    {
        public const string ReuseIdentifier = "ReviewTblCell";
        
        public ReviewTableCell()
            : base(ReuseIdentifier)
        {
        }

        protected ReviewTableCell(IntPtr handle)
            : base(handle)
        {
        }

        protected override IEnumerable<IBinding> Register()
        {
            yield return this.lblAuthor.BindText(this.Model, m => m.PersonName);
            yield return this.lblComment.BindText(this.Model, m => m.Message);
            yield return this.BindRating(this.Model, m => m.Rating, this.imgRating1, this.imgRating2, this.imgRating3, this.imgRating4, this.imgRating5);
        }

        public static ReviewTableCell CreateCell(UITableView table, NSIndexPath index, ReviewModel review)
        {
            return CreateCell<ReviewTableCell, ReviewModel>(ReuseIdentifier, table, index, review);
        }
    }
}