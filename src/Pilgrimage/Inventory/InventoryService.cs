using System;
using System.Collections.Generic;
using System.IO;
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

    /// <inheritdoc />
    public event EventHandler<InventoryChangedEventArgs> InventoryChanged = delegate { };

    /// <inheritdoc />
    public async Task<Result> Collect(PilgrimPlayer player, Item item, int count)
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

    /// <inheritdoc />
    public Task<bool> HasItem(PilgrimPlayer player, int itemId, int requiredNumber)
    {
        int totalItems = 0;

        foreach (Bag bag in player.Inventory)
        {
            foreach (BagSlot slot in bag.Slots)
            {
                foreach (BagItem item in slot.Items)
                {
                    if (item.Id == itemId)
                    {
                        totalItems += item.Count;
                    }
                }
            }
        }

        return Task.FromResult(requiredNumber <= totalItems);
    }

    /// <inheritdoc />
    public async Task<Result<PilgrimPlayer>> LoadInventory(Stream s)
    {
        if (_serializer is null)
        {
            return Result.Fail<PilgrimPlayer>("The serializer was not specified.");
        }

        Result<PilgrimPlayer> player = await _serializer.Deserialize(s);
        InventoryChanged(this, new InventoryChangedEventArgs { IsLoading = true });
        return player;
    }

    /// <inheritdoc />
    public async Task<Result> SaveInventory(Stream s, PilgrimPlayer player)
    {
        if (_serializer is null)
        {
            return Result.Fail<PilgrimPlayer>("The serializer was not specified.");
        }

        await _serializer.Serialize(s, player);
        return Result.Ok();
    }
}
