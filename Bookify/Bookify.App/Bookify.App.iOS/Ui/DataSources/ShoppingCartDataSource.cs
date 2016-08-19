using System;
using Bookify.App.Core.ViewModels;
using Bookify.App.iOS.Ui.TableCells;
using Foundation;
using UIKit;

namespace Bookify.App.iOS.Ui.DataSources
{
    public class ShoppingCartDataSource : UITableViewSource
    {
        private readonly ShoppingCartViewModel viewModel;

        public ShoppingCartDataSource(ShoppingCartViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cartItem = this.viewModel.CartItems[indexPath.Row];
            return CartItemTableCell.CreateCell(tableView, indexPath, cartItem);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (section == 0)
            {
                return this.viewModel.CartItems.Count;
            }
            return 0;
        }
        
        public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewRowAction btnRemove = UITableViewRowAction.Create(
                UITableViewRowActionStyle.Destructive,
                "Fjern én",
                (action, path) =>
                {
                    if (path.Section != 0)
                    {
                        return;
                    }
                    var cartItem = this.viewModel.CartItems[indexPath.Row];
                    if (cartItem.Quantity > 1)
                    {
                        cartItem.Quantity--;
                        return;
                    }

                    tableView.BeginUpdates();
                    this.viewModel.RemoveOne(cartItem);
                    tableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Left);
                    tableView.EndUpdates();
                });
            return new[] { btnRemove };
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return indexPath.Section == 0;
        }
    }
}