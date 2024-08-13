#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace Pilgrimage;

public static class PlayerExtensions
{
    public static bool IsQuestInProgress(this Player player, int questId)
    {
        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == questId);
        if (state is null)
        {
            return false;
        }

        return state.State == QuestState.InProgress;
    }

    public static List<Bag> Clone(this List<Bag> bags)
    {
        List<Bag> clone = new();

        foreach (Bag bag in bags)
        {
            clone.Add(bag.Clone());
        }

        return clone;
    }

    public static List<BagSlot> Clone(this List<BagSlot> slots)
    {
        List<BagSlot> clone = new();

        foreach (BagSlot slot in slots)
        {
            clone.Add(slot.Clone());
        }

        return clone;
    }

    public static List<BagItem> Clone(this List<BagItem> items)
    {
        List<BagItem> clone = new();

        foreach (BagItem item in items)
        {
            clone.Add(item.Clone());
        }

        return clone;
    }

    public static Bag Clone(this Bag bag)
    {
        return new Bag
        {
            MaxSlots = bag.MaxSlots,
            MaxWeight = bag.MaxWeight,
            Slots = bag.Slots.Clone()
        };
    }

    public static BagItem Clone(this BagItem item)
        => new()
        {
            Count = item.Count,
            Id = item.Id,
            Weight = item.Weight
        };

    public static BagSlot Clone(this BagSlot slot)
        => new()
        {
            Id = slot.Id,
            Items = slot.Items.Clone()
        };

    public static bool CanInteract(this Player player, Quest quest)
        => player.IsEligibleToStartQuest(quest);

    public static int GetCollectedItemsCount(this Player player, Quest quest)
    {
        int count = 0;

        foreach (QuestRequiredItem requiredItem in quest.RequiredItems)
        {
            int collectedCount = player.Inventory.GetCollectedCount(requiredItem.Id);
            count += collectedCount;
        }

        return count;
    }

    public static bool HasCollectedItems(this Player player, Quest quest)
    {
        foreach (QuestRequiredItem item in quest.RequiredItems)
        {
            if (!player.Inventory.HasItem(item.Id, item.Count))
            {
                return false;
            }
        }

        return true;
    }

    public static void RemoveCollectedItems(this Player player, Quest quest)
    {
        foreach (QuestRequiredItem item in quest.RequiredItems)
        {
            player.Inventory.RemoveItem(item.Id, item.Count);
        }
    }

    public static bool HasCompletedPreRequistes(this Player player, List<Quest> quests, Quest quest)
    {
        foreach (int preReqQuestId in quest.PreReqQuests)
        {
            Quest? preReqQuest = quests.FirstOrDefault(it => it.Id == preReqQuestId);
            if (preReqQuest is null)
            {
                return false;
            }

            if (!player.HasCompletedQuest(preReqQuest))
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsEligibleToStartQuest(this Player player, Quest quest)
        => player.Level > quest.MinLevel && !player.HasStartedQuest(quest);

    public static bool HasStartedQuest(this Player player, Quest quest)
        => player.Quests.Any(it => it.Id == quest.Id && it.State == QuestState.InProgress);

    public static bool HasCompletedQuest(this Player player, Quest quest)
        => player.Quests.Any(it => it.Id == quest.Id && it.State == QuestState.Completed);
}
