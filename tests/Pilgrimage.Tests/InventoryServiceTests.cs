namespace Pilgrimage.Tests;

public class InventoryServiceTests : InventoryTestClassBase
{
    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_Collect_Item_Into_Inventory()
    {
        await CreatePlayer();
        Item itemToCollect = new() { Id = 1 };

        IInventoryService service = new InventoryService(new FakeInventorySerializer());
        await AddBag(1);

        var result = await service.Collect(_player, itemToCollect, 1);
        var hasItem = await service.HasItem(_player, itemToCollect.Id, 1);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(hasItem);
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_Collect_TwoDifferentItem2_Into_Inventory()
    {
        await CreatePlayer();
        Item itemToCollect = new() { Id = 1 };
        Item secondItemToCollect = new() { Id = 2 };

        IInventoryService service = new InventoryService(new FakeInventorySerializer());
        await AddBag(2);

        var result = await service.Collect(_player, itemToCollect, 1);
        var result2 = await service.Collect(_player, secondItemToCollect, 2);
        var hasItem = await service.HasItem(_player, itemToCollect.Id, 1);
        var hasSecondItem = await service.HasItem(_player, secondItemToCollect.Id, 2);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(result2.Success);
        Assert.IsTrue(hasItem);
        Assert.IsTrue(hasSecondItem);
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_CombineCollect_Item_Into_Inventory()
    {
        await CreatePlayer();
        Item itemToCollect = new() { Id = 1 };

        IInventoryService service = new InventoryService(new FakeInventorySerializer());
        await AddBag(1);

        var result = await service.Collect(_player, itemToCollect, 1);
        var result2 = await service.Collect(_player, itemToCollect, 2);
        var hasItem = await service.HasItem(_player, itemToCollect.Id, 3);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(hasItem);
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Cannot_Collect_Item_Into_Inventory_NoSlots()
    {
        await CreatePlayer();
        Item itemToCollect = new() { Id = 1 };

        IInventoryService service = new InventoryService(new FakeInventorySerializer());

        var result = await service.Collect(_player, itemToCollect, 1);
        var hasItem = await service.HasItem(_player, itemToCollect.Id, 1);

        Assert.IsTrue(result.IsFailure);
        Assert.IsFalse(hasItem);
    }
}
