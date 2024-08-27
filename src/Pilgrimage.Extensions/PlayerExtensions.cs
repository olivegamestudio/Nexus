#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace Pilgrimage;

/// <summary>
/// Provides extension methods for the <see cref="PilgrimPlayer"/> class.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    /// Determines whether the specified quest is in progress for the player.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="questId">The unique identifier of the quest.</param>
    /// <returns><c>true</c> if the quest is in progress; otherwise, <c>false</c>.</returns>
    public static bool IsQuestInProgress(this PilgrimPlayer player, int questId)
    {
        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == questId);
        if (state is null)
        {
            return false;
        }

        return state.State == QuestState.InProgress;
    }

    /// <summary>
    /// Creates a deep clone of the list of bags.
    /// </summary>
    /// <param name="bags">The list of bags.</param>
    /// <returns>A deep clone of the list of bags.</returns>
    public static List<Bag> Clone(this List<Bag> bags)
    {
        List<Bag> clone = new();

        foreach (Bag bag in bags)
        {
            clone.Add(bag.Clone());
        }

        return clone;
    }

    /// <summary>
    /// Creates a deep clone of the list of bag slots.
    /// </summary>
    /// <param name="slots">The list of bag slots.</param>
    /// <returns>A deep clone of the list of bag slots.</returns>
    public static List<BagSlot> Clone(this List<BagSlot> slots)
    {
        List<BagSlot> clone = new();

        foreach (BagSlot slot in slots)
        {
            clone.Add(slot.Clone());
        }

        return clone;
    }

    /// <summary>
    /// Creates a deep clone of the list of bag items.
    /// </summary>
    /// <param name="items">The list of bag items.</param>
    /// <returns>A deep clone of the list of bag items.</returns>
    public static List<BagItem> Clone(this List<BagItem> items)
    {
        List<BagItem> clone = new();

        foreach (BagItem item in items)
        {
            clone.Add(item.Clone());
        }

        return clone;
    }

    /// <summary>
    /// Creates a deep clone of the bag.
    /// </summary>
    /// <param name="bag">The bag.</param>
    /// <returns>A deep clone of the bag.</returns>
    public static Bag Clone(this Bag bag)
    {
        return new Bag
        {
            MaxSlots = bag.MaxSlots,
            MaxWeight = bag.MaxWeight,
            Slots = bag.Slots.Clone()
        };
    }

    /// <summary>
    /// Creates a deep clone of the bag item.
    /// </summary>
    /// <param name="item">The bag item.</param>
    /// <returns>A deep clone of the bag item.</returns>
    public static BagItem Clone(this BagItem item)
        => new()
        {
            Count = item.Count,
            Id = item.Id,
            Weight = item.Weight
        };

    /// <summary>
    /// Creates a deep clone of the bag slot.
    /// </summary>
    /// <param name="slot">The bag slot.</param>
    /// <returns>A deep clone of the bag slot.</returns>
    public static BagSlot Clone(this BagSlot slot)
        => new()
        {
            Id = slot.Id,
            Items = slot.Items.Clone()
        };

    /// <summary>
    /// Determines whether the player can interact with the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player can interact with the quest; otherwise, <c>false</c>.</returns>
    public static bool CanInteract(this PilgrimPlayer player, Quest quest)
        => player.IsEligibleToStartQuest(quest);

    /// <summary>
    /// Gets the total count of collected items required for the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns>The total count of collected items required for the quest.</returns>
    public static int GetCollectedItemsCount(this PilgrimPlayer player, Quest quest)
    {
        int count = 0;

        foreach (QuestRequiredItem requiredItem in quest.RequiredItems)
        {
            int collectedCount = player.Inventory.GetCollectedCount(requiredItem.Id);
            count += collectedCount;
        }

        return count;
    }

    /// <summary>
    /// Determines whether the player has collected all required items for the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player has collected all required items; otherwise, <c>false</c>.</returns>
    public static bool HasCollectedItems(this PilgrimPlayer player, Quest quest)
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

    /// <summary>
    /// Removes the collected items required for the specified quest from the player's inventory.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    public static void RemoveCollectedItems(this PilgrimPlayer player, Quest quest)
    {
        foreach (QuestRequiredItem item in quest.RequiredItems)
        {
            player.Inventory.RemoveItem(item.Id, item.Count);
        }
    }

    /// <summary>
    /// Determines whether the player has completed all prerequisites for the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quests">The list of all quests.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player has completed all prerequisites; otherwise, <c>false</c>.</returns>
    public static bool HasCompletedPreRequistes(this PilgrimPlayer player, List<Quest> quests, Quest quest)
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

    /// <summary>
    /// Determines whether the player is eligible to start the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player is eligible to start the quest; otherwise, <c>false</c>.</returns>
    public static bool IsEligibleToStartQuest(this PilgrimPlayer player, Quest quest)
        => player.Level > quest.MinLevel && !player.HasStartedQuest(quest);

    /// <summary>
    /// Determines whether the player has started the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player has started the quest; otherwise, <c>false</c>.</returns>
    public static bool HasStartedQuest(this PilgrimPlayer player, Quest quest)
        => player.Quests.Any(it => it.Id == quest.Id && it.State == QuestState.InProgress);

    /// <summary>
    /// Determines whether the player has completed the specified quest.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="quest">The quest.</param>
    /// <returns><c>true</c> if the player has completed the quest; otherwise, <c>false</c>.</returns>
    public static bool HasCompletedQuest(this PilgrimPlayer player, Quest quest)
        => player.Quests.Any(it => it.Id == quest.Id && it.State == QuestState.Completed);
}
