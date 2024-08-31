using Utility;

namespace Pilgrimage.Inventory.Tests;

public class FakeInventorySerializer : IInventorySerializer
{
    public Task<ObjectResult<PilgrimPlayer>> Deserialize(Stream s)
    {
        return Task.FromResult<ObjectResult<PilgrimPlayer>>(OkObjectResult<PilgrimPlayer>.Ok(new PilgrimPlayer()));
    }

    Task<Result> IInventorySerializer.Serialize(Stream s, PilgrimPlayer player)
    {
        return Task.FromResult<Result>(OkResult.Ok());
    }
}
