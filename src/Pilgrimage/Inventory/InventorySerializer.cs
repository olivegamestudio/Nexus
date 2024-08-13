using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public class InventorySerializer : IInventorySerializer
{
    readonly IInventoryFileStorage _storage;

    public InventorySerializer(IInventoryFileStorage storage)
    {
        _storage = storage;
    }

    public Task<Result<Player>> Deserialize()
    {
        if (File.Exists(_storage.Path))
        {
            using (Stream s = new FileStream(_storage.Path, FileMode.Open))
            {
                return Task.FromResult(Result.Ok<Player>(JsonSerializer.Deserialize<Player>(s, JsonSerializerOptions.Default)));
            }
        }

        return Task.FromResult(Result.Fail<Player>("Save game not found."));
    }

    public Task<Result> Serialize(Player player)
    {
        using (Stream s = new FileStream(_storage.Path, FileMode.Create))
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            JsonSerializer.Serialize(s, player, options);
        }

        return Task.FromResult(Result.Ok());
    }
}
