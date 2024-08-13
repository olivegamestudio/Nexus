using System.Collections.Generic;

namespace Pilgrimage;

public static class BagExtensions
{
    public static bool HasItem(this List<Bag> bags, int itemId, int count)
    {
        return bags.GetCollectedCount(itemId) >= count;
    }

    public static void RemoveItem(this List<Bag> bags, int itemId, int count)
    {
        foreach (Bag bag in bags)
        {
            foreach (BagSlot slot in bag.Slots)
            {
                foreach (BagItem item in slot.Items)
                {
                    if (item.Id == itemId)
                    {
                        if (item.Count >= count)
                        {
                            item.Count -= count;
                            return;
                        }

                        count -= item.Count;
                        item.Count = 0;
                    }
                }
            }
        }
    }

    public static int GetCollectedCount(this List<Bag> bags, int itemId)
    {
        int collected = 0;

        foreach (Bag bag in bags)
        {
            foreach (BagSlot slot in bag.Slots)
            {
                foreach (BagItem item in slot.Items)
                {
                    if (item.Id == itemId)
                    {
                        collected += item.Count;
                    }
                }
            }
        }

        return collected;
    }
}
