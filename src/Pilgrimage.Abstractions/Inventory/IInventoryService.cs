using System;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public interface IInventoryService
{
    event EventHandler<InventoryChangedEventArgs> InventoryChanged;

    Task<Result> Collect(Player player, Item item, int count);

    Task<bool> HasItem(Player player, int itemId, int count);

    Task<Result<Player>> LoadInventory();
}
