using System;
using System.IO;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

/// <summary>
/// Interface for managing player inventory operations.
/// </summary>
public interface IInventoryService
{
    /// <summary>
    /// Event triggered when the inventory changes.
    /// </summary>
    event EventHandler<InventoryChangedEventArgs> InventoryChanged;

    /// <summary>
    /// Collects a specified number of items for the player.
    /// </summary>
    /// <param name="player">The player collecting the items.</param>
    /// <param name="item">The item to be collected.</param>
    /// <param name="count">The number of items to collect.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating the success or failure of the operation.</returns>
    Task<Result> Collect(PilgrimPlayer player, Item item, int count);

    /// <summary>
    /// Checks if the player has the required number of a specific item.
    /// </summary>
    /// <param name="player">The player whose inventory is being checked.</param>
    /// <param name="itemId">The ID of the item to check.</param>
    /// <param name="requiredNumber">The required number of the item.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the player has the required number of the item.</returns>
    Task<bool> HasItem(PilgrimPlayer player, int itemId, int requiredNumber);

    /// <summary>
    /// Loads the player's inventory from the provided stream.
    /// </summary>
    /// <param name="s">The stream from which the inventory data will be loaded.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{PilgrimPlayer}"/> with the loaded player data.</returns>
    Task<ObjectResult<PilgrimPlayer>> LoadInventory(Stream s);

    /// <summary>
    /// Saves the player's inventory to the provided stream.
    /// </summary>
    /// <param name="s">The stream to which the inventory data will be saved.</param>
    /// <param name="player">The player whose inventory data is to be saved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result"/> indicating the success or failure of the operation.</returns>
    Task<Result> SaveInventory(Stream s, PilgrimPlayer player);
}
