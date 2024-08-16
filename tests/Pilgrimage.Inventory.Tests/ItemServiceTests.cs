using Musts;
using SparkIO;
using Utility;

namespace Pilgrimage.Inventory.Tests;

public class ItemServiceTests
{
    [Test]
    public async Task AddItem_Then_GetItem_IsSuccess()
    {
        ItemService items = new(new FileSystemFake());
        Rock rock = new();
        items.AddItem(rock);

        Result<Item> result = await items.GetItem(rock.Id);
        result.MustBeSuccess();
    }

    [Test]
    public async Task GetItem_ThatWasNotAdded_Returns_Failure()
    {
        ItemService items = new(new FileSystemFake());

        Rock rock = new();
        Result<Item> result = await items.GetItem(rock.Id);
        result.MustBeFailure();
    }
}
