using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public class InventoryService : IInventoryService
{
    readonly IInventorySerializer? _serializer;

    public InventoryService(IInventorySerializer serializer = null)
    {
        _serializer = serializer;
    }
    
    /// <summary>
    /// This function first looks for an existing slot with related items and combines.
    /// If there are no related items, then it finds a slot that can be used.
    /// </summary>
    /// <param name="bags"></param>
    /// <param name="item"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    static Task<BagItem?> FindSpace(List<Bag> bags, Item item, int count)
    {
        // find a slot that can accommodate this item
        foreach (Bag bag in bags)
        {
            foreach (BagSlot slot in bag.Slots)
            {
                // combine if possible and free space
                foreach (BagItem bagItem in slot.Items)
                {
                    if (item.Id == bagItem.Id)
                    {
                        if (item.MaxItemsPerSlot > slot.GetTotalItems() + count &&
                            item.MaxWeightPerSlot > slot.GetTotalWeight() + item.Weight)
                        {
                            return Task.FromResult((BagItem?)bagItem);
                        }
                    }
                }
            }
        }

        // create a new slot if permitted
        foreach (Bag bag in bags)
        {
            if (bag.Slots.Count < bag.MaxSlots)
            {
                BagSlot newSlot = new();
                bag.Slots.Add(newSlot);

                BagItem bagItem = new();
                newSlot.Items.Add(bagItem);

                return Task.FromResult((BagItem?)bagItem);
            }
        }

        // no room available
        return Task.FromResult((BagItem?)null);
    }

    public event EventHandler<InventoryChangedEventArgs> InventoryChanged = delegate { };

    public async Task<Result> Collect(Player player, Item item, int count)
    {
        List<Bag> inventory = player.Inventory.Clone();

        // find a suitable slot for this item
        BagItem? freeSlot = await FindSpace(inventory, item, count);
        if (freeSlot is null)
        {
            return Result.Fail("No free slot available.");
        }

        // found a suitable slot add item to it
        freeSlot.Id = item.Id;
        freeSlot.Count += count;
        freeSlot.Weight += item.Weight;

        // add item to the slot provided.
        player.Inventory = inventory;

        InventoryChanged(this, new InventoryChangedEventArgs());
        return Result.Ok();
    }

    public Task<bool> HasItem(Player player, int itemId, int count)
    {
        int total = 0;

        foreach (Bag bag in player.Inventory)
        {
            foreach (BagSlot slot in bag.Slots)
            {
                foreach (BagItem item in slot.Items)
                {
                    if (item.Id == itemId)
                    {
                        total += item.Count;
                    }
                }
            }
        }

        return Task.FromResult(total >= count);
    }

    public async Task<Result<Player>> LoadInventory()
    {
        Result<Player> player = await _serializer.Deserialize();
        InventoryChanged(this, new InventoryChangedEventArgs { IsLoading = true });
        return player;
    }
}
