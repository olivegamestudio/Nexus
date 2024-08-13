using Utility;

namespace Pilgrimage.Tests;

public class FakeInventorySerializer : IInventorySerializer
{
    public Task<Result<Player>> Deserialize()
    {
        throw new NotImplementedException();
    }

    Task<Result> IInventorySerializer.Serialize(Player player)
    {
        throw new NotImplementedException();
    }
}
