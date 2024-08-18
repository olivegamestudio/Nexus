using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public class InventorySerializer : IInventorySerializer
{
    /// <inheritdoc />
    public Task<Result<PilgrimPlayer>> Deserialize(Stream s)
    {
        return Task.FromResult(Result.Ok<PilgrimPlayer>(JsonSerializer.Deserialize<PilgrimPlayer>(s, JsonSerializerOptions.Default)));
    }

    /// <inheritdoc />
    public Task<Result> Serialize(Stream s, PilgrimPlayer player)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        JsonSerializer.Serialize(s, player, options);
        return Task.FromResult(Result.Ok());
    }
}
