using Musts;
using Utility;

namespace Pilgrimage.Tests;

public class InventoryServiceTests : InventoryTestClassBase
{
    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_Collect_Item_Into_Inventory()
    {
        Player player = await CreatePlayer();
        Item rock = new Rock();

        InventoryService inventory = new(new FakeInventorySerializer());
        Bag bag = await AddBag(player, 1);

        Result result = await inventory.Collect(player, rock, 1);
        bool hasItem = await inventory.HasItem(player, rock.Id, 1);

        result.Success.MustBeTrue();
        hasItem.MustBeTrue();
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_Collect_TwoDifferentItem2_Into_Inventory()
    {
        Player player = await CreatePlayer();
        Item rock = new Rock();
        Item asteroid = new Asteroid();

        InventoryService inventory = new(new FakeInventorySerializer());
        Bag bag = await AddBag(player, 2);

        Result result = await inventory.Collect(player, rock, 1);
        Result result2 = await inventory.Collect(player, asteroid, 2);
        bool hasItem = await inventory.HasItem(player, rock.Id, 1);
        bool hasSecondItem = await inventory.HasItem(player, asteroid.Id, 2);

        result.Success.MustBeTrue();
        result2.Success.MustBeTrue();
        hasItem.MustBeTrue();
        hasSecondItem.MustBeTrue();
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Can_CombineCollect_Item_Into_Inventory()
    {
        Player player = await CreatePlayer();
        Item itemToCollect = new() { Id = 1 };

        IInventoryService inventory = new InventoryService(new FakeInventorySerializer());
        Bag bag = await AddBag(player, 1);

        Result result = await inventory.Collect(player, itemToCollect, 1);
        Result result2 = await inventory.Collect(player, itemToCollect, 2);
        bool hasItem = await inventory.HasItem(player, itemToCollect.Id, 3);

        result.Success.MustBeTrue();
        hasItem.MustBeTrue();
    }

    [Test]
    [Parallelizable(ParallelScope.None)]
    public async Task Cannot_Collect_Item_Into_Inventory_NoSlots()
    {
        Player player = await CreatePlayer();
        Item rock = new Rock();

        InventoryService inventory = new(new FakeInventorySerializer());

        Result result = await inventory.Collect(player, rock, 1);
        bool hasItem = await inventory.HasItem(player, rock.Id, 1);

        result.Success.MustBeFalse();
        hasItem.MustBeFalse();
    }
}
