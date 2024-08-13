using System.Collections.Generic;

namespace Pilgrimage;

public class Bag
{
    public List<BagSlot> Slots { get; set; } = new();

    public int MaxWeight { get; set; } = int.MaxValue;

    public int MaxSlots { get; set; } = int.MaxValue;
}
