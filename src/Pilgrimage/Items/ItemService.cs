using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SparkIO;
using Utility;

namespace Pilgrimage;

public class ItemService : IItemService
{
    readonly IFileSystem _fileSystem;
    readonly ILogger<ItemService>? _logger;
    readonly List<Item> _items = new();

    public bool HasLoaded { get; set; }

    /// <inheritdoc />
    public event EventHandler ItemsChanged = delegate { };

    public ItemService(IFileSystem fileSystem, ILogger<ItemService> logger = null)
    {
        _fileSystem = fileSystem;
        _logger = logger;
    }

    public Task<Result<Item>> GetItem(int id)
    {
        Item item = _items.FirstOrDefault(it => it.Id == id);
        if (item is null)
        {
            return Task.FromResult(Result.Fail<Item>("The item was not found."));
        }

        return Task.FromResult(Result.Ok<Item>(item));
    }

    public async Task<Result> LoadItems()
    {
        for (int n = 1; n < 9; n++)
        {
            Stream s = await _fileSystem.OpenStreamAsync($"item{n}.json");
            Item item = await JsonSerializer.DeserializeAsync<Item>(s, JsonSerializerOptions.Default);
            _items.Add(item);
        }

        HasLoaded = true;
        ItemsChanged.Invoke(this, EventArgs.Empty);
        return Result.Ok();
    }

    /// <inheritdoc />
    public void AddItem(Item item)
    {
        _items.Add(item);
        ItemsChanged.Invoke(this, EventArgs.Empty);
    }
}
