using System;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

/// <summary>  
/// Provides an interface for item-related operations and events.  
/// </summary>  
public interface IItemService
{
    /// <summary>  
    /// Loads all items.  
    /// </summary>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the load operation.</returns>  
    Task<Result> LoadItems();

    /// <summary>  
    /// Gets an item by its unique identifier.  
    /// </summary>  
    /// <param name="id">The unique identifier of the item.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the item.</returns>  
    Task<Result<Item>> GetItem(int id);

    /// <summary>  
    /// Gets or sets a value indicating whether items have been loaded.  
    /// </summary>  
    bool HasLoaded { get; set; }

    /// <summary>  
    /// Adds an item to the service.  
    /// </summary>  
    /// <param name="item">The item to add.</param>  
    void AddItem(Item item);

    /// <summary>  
    /// Raised when the items have changed.  
    /// </summary>  
    event EventHandler ItemsChanged;
}
