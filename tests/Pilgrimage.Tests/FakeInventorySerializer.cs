using Utility;

namespace Pilgrimage.Tests;

public class FakeInventorySerializer : IInventorySerializer
{
    public Task<Result<Player>> Deserialize()
    {
        return Task.FromResult(Result.Ok(new Player()));
    }

    Task<Result> IInventorySerializer.Serialize(Player player)
    {
        return Task.FromResult(Result.Ok());
    }
}
