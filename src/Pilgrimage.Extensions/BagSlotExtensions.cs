using System.Linq;

namespace Pilgrimage;

public static class BagSlotExtensions
{
    public static int GetTotalWeight(this BagSlot slot)
        => slot.Items.Sum(item => item.Weight);

    public static int GetTotalItems(this BagSlot slot)
        => slot.Items.Sum(item => item.Count);
}
