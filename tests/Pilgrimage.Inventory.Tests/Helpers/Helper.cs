namespace Pilgrimage.Inventory.Tests;

public static class Helper
{
    public static Task<Bag> CreateBag(this Player player, int maxSlots)
    {
        Bag bag = new() { MaxSlots = maxSlots };
        player.Inventory.Add(bag);
        return Task.FromResult(bag);
    }
}