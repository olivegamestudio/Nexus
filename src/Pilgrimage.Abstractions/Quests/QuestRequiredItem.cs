namespace Pilgrimage;

/// <summary>
/// Represents an item required to complete a quest.
/// </summary>
public class QuestRequiredItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the required item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the count of the required item.
    /// </summary>
    public int Count { get; set; }
}
