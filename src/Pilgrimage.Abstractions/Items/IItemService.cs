using System;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

public interface IItemService
{
    Task<Result> LoadItems();

    Task<Result<Item>> GetItem(int id);
    
    bool HasLoaded { get; set; }

    /// <summary>Add item to service.</summary>
    void AddItem(Item item);

    /// <summary>The items have changed.</summary>
    event EventHandler ItemsChanged;
}
