using Musts;
using Utility;
using Xunit;

namespace Pilgrimage.Inventory.Tests;

public class InventoryServiceTests : InventoryTestClassBase
{
    [Fact]
    public async Task Can_Collect_Item_Into_Inventory()
    {
        PilgrimPlayer player = await CreatePlayer();
        IInventoryService inventory = await CreateInventory();

        Bag newlyCreatedBag = await player.CreateBag(1);

        Item rock = new Rock();
        Result result = await inventory.Collect(player, rock, 1);
        bool hasItem = await inventory.HasItem(player, rock.Id, 1);

        result.Success.MustBeTrue();
        hasItem.MustBeTrue();
    }

    [Fact]
    public async Task Can_Collect_TwoDifferentItem2_Into_Inventory()
    {
        PilgrimPlayer player = await CreatePlayer();
        IInventoryService inventory = await CreateInventory();

        Item rock = new Rock();
        Item asteroid = new Asteroid();

        Bag newlyCreatedBag = await player.CreateBag(2);

        Result rockResult = await inventory.Collect(player, rock, 1);
        Result asteroidResult = await inventory.Collect(player, asteroid, 2);
       
        bool hasRock = await inventory.HasItem(player, rock.Id, 1);
        bool hasAsteroid = await inventory.HasItem(player, asteroid.Id, 2);

        rockResult.Success.MustBeTrue();
        asteroidResult.Success.MustBeTrue();
        hasRock.MustBeTrue();
        hasAsteroid.MustBeTrue();
    }

    [Fact]
    public async Task Can_CombineCollect_Item_Into_Inventory()
    {
        PilgrimPlayer player = await CreatePlayer();
        IInventoryService inventory = await CreateInventory();

        Bag newlyCreatedBag = await player.CreateBag(1);

        Item rock = new Rock();

        Result oneRock = await inventory.Collect(player, rock, 1);
        Result twoRocks = await inventory.Collect(player, rock, 2);
        bool hasThreeItems = await inventory.HasItem(player, rock.Id, 3);

        oneRock.Success.MustBeTrue();
        twoRocks.Success.MustBeTrue();
        hasThreeItems.MustBeTrue();
    }

    [Fact]
    public async Task Cannot_Collect_Item_Into_Inventory_NoSlots()
    {
        PilgrimPlayer player = await CreatePlayer();
        IInventoryService inventory = await CreateInventory();

        Item rock = new Rock();

        Result collectResult = await inventory.Collect(player, rock, 1);
        bool hasItem = await inventory.HasItem(player, rock.Id, 1);

        collectResult.Success.MustBeFalse();
        hasItem.MustBeFalse();
    }
}
