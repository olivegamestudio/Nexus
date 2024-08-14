namespace Pilgrimage.Tests;

public abstract class InventoryTestClassBase
{
    protected Task<Player> CreatePlayer()
    {
        return Task.FromResult(new Player());
    }

    protected Task<Bag> AddBag(Player player, int maxSlots)
    {
        Bag bag = new() { MaxSlots = maxSlots };
        player.Inventory.Add(bag);
        return Task.FromResult(bag);
    }
}
