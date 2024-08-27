using System.Linq;

namespace Pilgrimage;

/// <summary>
/// Provides extension methods for the <see cref="BagSlot"/> class.
/// </summary>
public static class BagSlotExtensions
{
    /// <summary>
    /// Gets the total weight of all items in the bag slot.
    /// </summary>
    /// <param name="slot">The bag slot.</param>
    /// <returns>The total weight of all items in the bag slot.</returns>
    public static int GetTotalWeight(this BagSlot slot)
        => slot.Items.Sum(item => item.Weight);

    /// <summary>
    /// Gets the total count of all items in the bag slot.
    /// </summary>
    /// <param name="slot">The bag slot.</param>
    /// <returns>The total count of all items in the bag slot.</returns>
    public static int GetTotalItems(this BagSlot slot)
        => slot.Items.Sum(item => item.Count);
}
