using System.Collections.Generic;

namespace Pilgrimage;

public class BagSlot
{
    public int Id { get; set; }

    public List<BagItem> Items { get; set; } = new();
}
