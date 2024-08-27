namespace Pilgrimage;

/// <summary>  
/// Represents an item in a bag.  
/// </summary>  
public class BagItem
{
    /// <summary>  
    /// Gets or sets the unique identifier for the bag item.  
    /// </summary>  
    public int Id { get; set; }

    /// <summary>  
    /// Gets or sets the count of the bag item.  
    /// </summary>  
    public int Count { get; set; }

    /// <summary>  
    /// Gets or sets the weight of the bag item.  
    /// </summary>  
    public int Weight { get; set; }
}
