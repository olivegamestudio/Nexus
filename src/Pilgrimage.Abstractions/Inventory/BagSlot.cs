using System.Collections.Generic;

namespace Pilgrimage;

/// <summary>
/// Represents a slot in a bag that can hold multiple items.
/// </summary>
public class BagSlot
{
    /// <summary>
    /// Gets or sets the unique identifier for the bag slot.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the bag slot.
    /// </summary>
    public List<BagItem> Items { get; set; } = new();
}
