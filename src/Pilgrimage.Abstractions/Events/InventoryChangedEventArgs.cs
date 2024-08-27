using System;

namespace Pilgrimage;

/// <summary>
/// Provides data for the InventoryChanged event.
/// </summary>
public class InventoryChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets a value indicating whether the inventory is loading.
    /// </summary>
    public bool IsLoading { get; set; }
}
