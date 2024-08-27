namespace Pilgrimage;

/// <summary>  
/// Represents an item in the game.  
/// </summary>  
public class Item
{
    /// <summary>  
    /// Gets or sets the unique identifier for the item.  
    /// </summary>  
    public int Id { get; set; }

    /// <summary>  
    /// Gets or sets the sell value of the item.  
    /// </summary>  
    public int SellValue { get; set; }

    /// <summary>  
    /// Gets or sets the category of the item.  
    /// </summary>  
    public int ItemCategory { get; set; }

    /// <summary>  
    /// Gets or sets a value indicating whether the item can be sold.  
    /// </summary>  
    public bool CanSell { get; set; }

    /// <summary>  
    /// Gets or sets the image associated with the item.  
    /// </summary>  
    public string Image { get; set; }

    /// <summary>  
    /// Gets or sets the maximum number of items per slot.  
    /// </summary>  
    public int MaxItemsPerSlot { get; set; } = int.MaxValue;

    /// <summary>  
    /// Gets or sets the maximum weight per slot.  
    /// </summary>  
    public int MaxWeightPerSlot { get; set; } = int.MaxValue;

    /// <summary>  
    /// Gets or sets the weight of the item.  
    /// </summary>  
    public int Weight { get; set; }
}
