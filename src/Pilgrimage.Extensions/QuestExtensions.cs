namespace Pilgrimage;

/// <summary>
/// Provides extension methods for the <see cref="Quest"/> class.
/// </summary>
public static class QuestExtensions
{
    /// <summary>
    /// Gets the total count of required items for the quest.
    /// </summary>
    /// <param name="quest">The quest.</param>
    /// <returns>The total count of required items for the quest.</returns>
    public static int GetRequiredItemsCount(this Quest quest)
    {
        int count = 0;

        foreach (QuestRequiredItem requiredItem in quest.RequiredItems)
        {
            count += requiredItem.Count;
        }

        return count;
    }
}
