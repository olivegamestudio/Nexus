using Utility;

namespace Pilgrimage.Tests;

public class ItemServiceTests
{
    [Test]
    public async Task AddItem_IsAdded_To_Service()
    {
        ItemService items = new(new FileSystemFake());
        Rock rock = new();
        items.AddItem(rock);

        Result<Item> item = await items.GetItem(rock.Id);
    }
}
