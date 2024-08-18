using Utility;

namespace Pilgrimage.Inventory.Tests;

public class FakeInventorySerializer : IInventorySerializer
{
    public Task<Result<PilgrimPlayer>> Deserialize(Stream s)
    {
        return Task.FromResult(Result.Ok(new PilgrimPlayer()));
    }

    Task<Result> IInventorySerializer.Serialize(Stream s, PilgrimPlayer player)
    {
        return Task.FromResult(Result.Ok());
    }
}
