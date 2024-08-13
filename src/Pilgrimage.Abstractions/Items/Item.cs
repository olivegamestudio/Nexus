namespace Pilgrimage;

public class Item
{
    public int Id { get; set; }

    public int SellValue { get; set; }

    public int ItemCategory { get; set; }

    public bool CanSell { get; set; }

    public string Image { get; set; }

    public int MaxItemsPerSlot { get; set; } = int.MaxValue;

    public int MaxWeightPerSlot { get; set; } = int.MaxValue;

    public int Weight { get; set; }
}
