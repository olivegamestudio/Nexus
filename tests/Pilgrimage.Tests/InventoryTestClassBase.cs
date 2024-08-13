namespace Pilgrimage.Tests;

public abstract class InventoryTestClassBase
{
    protected Player _player = new();

    protected Task CreatePlayer()
    {
        _player = new();
        return Task.CompletedTask;
    }

    protected Task AddBag(int maxSlots)
    {
        Bag bag = new() { MaxSlots = maxSlots };
        _player.Inventory.Add(bag);
        return Task.CompletedTask;
    }
}
