namespace Pilgrimage.Inventory.Tests;

public abstract class InventoryTestClassBase
{
    protected Task<Player> CreatePlayer()
    {
        return Task.FromResult(new Player());
    }

    protected Task<IInventoryService> CreateInventory()
    {
        return Task.FromResult((IInventoryService)new InventoryService(new FakeInventorySerializer()));
    }
}
