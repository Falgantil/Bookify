using System;
using System.Collections.Generic;

using Bookify.App.Core.ViewModels;
using Bookify.Common.Models;

using Foundation;

using Rope.Net;
using Rope.Net.iOS;

using UIKit;

namespace Bookify.App.iOS.Ui.TableCells
{
    public partial class GenreTableCell : ExtendedTableViewCell<GenreTableCell.GenreDtoViewModel>
    {
        public const string Reuseidentifier = "GenreTableCell";

        public GenreTableCell(IntPtr handle) : base(handle)
        {
        }

        public GenreTableCell() : base(Reuseidentifier)
        {
        }

        protected override IEnumerable<IBinding> Register()
        {
            yield return this.TextLabel.BindText(this.Model, vm => vm.Genre.Name);
        }

        public static GenreTableCell CreateCell(UITableView tableView, NSIndexPath index, GenreDto dto)
        {
            return CreateCell<GenreTableCell, GenreDtoViewModel>(Reuseidentifier, tableView, index,
                new GenreDtoViewModel(dto));
        }

        public class GenreDtoViewModel : BaseViewModel
        {
            public GenreDtoViewModel(GenreDto genre)
            {
                this.Genre = genre;
            }

            public GenreDto Genre { get; }
        }
    }
}