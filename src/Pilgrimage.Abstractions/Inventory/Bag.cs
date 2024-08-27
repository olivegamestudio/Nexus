using System.Collections.Generic;

namespace Pilgrimage;

/// <summary>
/// Represents a bag that can hold multiple slots of items.
/// </summary>
public class Bag
{
    /// <summary>
    /// Gets or sets the list of slots in the bag.
    /// </summary>
    public List<BagSlot> Slots { get; set; } = new();

    /// <summary>
    /// Gets or sets the maximum weight the bag can hold.
    /// </summary>
    public int MaxWeight { get; set; } = int.MaxValue;

    /// <summary>
    /// Gets or sets the maximum number of slots the bag can have.
    /// </summary>
    public int MaxSlots { get; set; } = int.MaxValue;
}
