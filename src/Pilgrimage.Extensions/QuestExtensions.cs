namespace Pilgrimage;

public static class QuestExtensions
{
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
