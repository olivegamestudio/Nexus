namespace Pilgrimage.Inventory.Tests;

public abstract class InventoryTestClassBase
{
    protected Task<PilgrimPlayer> CreatePlayer()
    {
        return Task.FromResult(new PilgrimPlayer());
    }

    protected Task<IInventoryService> CreateInventory()
    {
        return Task.FromResult((IInventoryService)new InventoryService(new FakeInventorySerializer()));
    }
}
