using System;

namespace Pilgrimage;

public class InventoryChangedEventArgs : EventArgs
{
    public bool IsLoading { get; set; }
}
