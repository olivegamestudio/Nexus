
using System.Collections.Generic;

namespace Pilgrimage;

/// <summary>  
/// Provides extension methods for the <see cref="Bag"/> class.  
/// </summary>  
public static class BagExtensions
{
    /// <summary>  
    /// Determines whether the specified bags contain the required count of the specified item.  
    /// </summary>  
    /// <param name="bags">The list of bags.</param>  
    /// <param name="itemId">The unique identifier of the item.</param>  
    /// <param name="count">The required count of the item.</param>  
    /// <returns><c>true</c> if the bags contain the required count of the item; otherwise, <c>false</c>.</returns>  
    public static bool HasItem(this List<Bag> bags, int itemId, int count)
    {
        return bags.GetCollectedCount(itemId) >= count;
    }

    /// <summary>  
    /// Removes the specified count of the specified item from the bags.  
    /// </summary>  
    /// <param name="bags">The list of bags.</param>  
    /// <param name="itemId">The unique identifier of the item.</param>  
    /// <param name="count">The count of the item to remove.</param>  
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

    /// <summary>  
    /// Gets the total count of the specified item in the bags.  
    /// </summary>  
    /// <param name="bags">The list of bags.</param>  
    /// <param name="itemId">The unique identifier of the item.</param>  
    /// <returns>The total count of the item in the bags.</returns>  
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
