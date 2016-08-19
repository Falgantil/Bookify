using System;
using System.Collections.Generic;
using System.ComponentModel;
using Foundation;
using Rope.Net;
using UIKit;

namespace Bookify.App.iOS.Ui.TableCells
{
    public abstract class ExtendedTableViewCell<T> : UITableViewCell where T : INotifyPropertyChanged
    {
        private readonly List<IBinding> bindings = new List<IBinding>();

        protected ExtendedTableViewCell(string reuseIdentifier) : base(UITableViewCellStyle.Default, reuseIdentifier)
        {
        }

        protected ExtendedTableViewCell(IntPtr handle)
            : base(handle)
        {
        }

        protected T Model { get; private set; }

        protected void Initialize(T book)
        {
            this.Unregister();
            this.Model = book;
            this.bindings.AddRange(this.Register());
        }

        protected virtual void Unregister()
        {
            if (this.Model == null)
            {
                return;
            }
            this.bindings.ForEach(b => b.Dispose());
            this.bindings.Clear();
        }

        protected abstract IEnumerable<IBinding> Register();
        
        public static TCell CreateCell<TCell, TModel>(string reuseIdentifier, UITableView table, NSIndexPath index, TModel book)
            where TModel : INotifyPropertyChanged
            where TCell : ExtendedTableViewCell<TModel>, new()
        {
            var cell = table.DequeueReusableCell(reuseIdentifier, index);
            var bookCell = cell as TCell ?? new TCell();
            bookCell.Initialize(book);
            return bookCell;
        }
    }
}