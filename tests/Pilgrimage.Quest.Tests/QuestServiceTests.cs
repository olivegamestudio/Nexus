using Musts;
using Utility;
using Pilgrimage;
using SparkIO;
using Xunit;

namespace Pilgrimage.Tests;

public class QuestServiceTests
{
    [Fact]
    public async Task Quest_IsNotPreRequisiteQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest firstQuest = new() { Id = 1 };
        await quests.AddQuest(firstQuest);

        Quest secondQuest = new() { Id = 2 };
        await quests.AddQuest(secondQuest);

        Result<bool> result = await quests.IsPreRequisite(secondQuest.Id, firstQuest.Id);
        result.MustBeSuccess();
        result.Value.MustBeFalse();
    }

    [Fact]
    public async Task Quest_IsPreRequisiteQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest firstQuest = new() { Id = 1 };
        await quests.AddQuest(firstQuest);

        Quest secondQuest = new() { Id = 2 };
        secondQuest.PreReqQuests.Add(firstQuest.Id);
        await quests.AddQuest(secondQuest);

        Result<bool> result = await quests.IsPreRequisite(secondQuest.Id, firstQuest.Id);
        result.MustBeSuccess();
        result.Value.MustBeTrue();
    }

    [Fact]
    public async Task Player_CompletesUnStartedQuest_Then_Fails()
    {
        IItemService items = new ItemService(new FileSystemFake());
        InventoryService inventory = new InventoryService();
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest quest = new() { Id = 1 };
        quest.RequiredItems.Add(new QuestRequiredItem { Id = 1, Count = 1 });
        await quests.AddQuest(quest);

        PilgrimPlayer player = new();

        Result result = await quests.CompleteQuest(player, items, inventory, quest.Id);
        result.MustBeFailure();
    }

    [Fact]
    public async Task Player_CompletesStartedQuest_Then_RemovesCollectedItems()
    {
        IInventoryService inventory = new InventoryService();
        IItemService items = new ItemService(new FileSystemFake());
        IQuestService quests = new QuestService(new FileSystemFake());

        items.AddItem(new Item { Id = 2 });

        Quest quest = new() { Id = 1 };
        quest.RewardItems.Add(new QuestRewardItem { Id = 2, Count = 1 });
        quest.RequiredItems.Add(new QuestRequiredItem { Id = 1, Count = 1 });
        await quests.AddQuest(quest);

        PilgrimPlayer player = new();
        Bag bag = new();
        player.Inventory.Add(bag);

        BagSlot slot = new();
        bag.Slots.Add(slot);

        BagItem item = new() { Id = 1, Count = 1 };
        slot.Items.Add(item);
        
        Result startQuestResult = await quests.StartQuest(player, quest.Id);
        startQuestResult.MustBeSuccess();

        Result completeQuestResult = await quests.CompleteQuest(player, items, inventory, quest.Id);
        completeQuestResult.MustBeSuccess();

        bool hasQuestItem = await inventory.HasItem(player, item.Id, 1);
        hasQuestItem.MustBeFalse();

        bool hasRewardItem = await inventory.HasItem(player, 2, 1);
        hasRewardItem.MustBeTrue();
    }

    [Fact]
    public async Task Player_StartQuest_Then_HasStartedQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest quest = new() { Id = 1 };
        await quests.AddQuest(quest);

        PilgrimPlayer player = new();

        Result startResult = await quests.StartQuest(player, quest.Id);
        Result hasStartedResult = await quests.HasStartedQuest(player, quest.Id);

        startResult.MustBeSuccess();
        hasStartedResult.MustBeSuccess();
    }

    [Fact]
    public async Task Player_NotStartQuest_Then_HasNotStartedQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest quest = new() { Id = 1 };
        await quests.AddQuest(quest);

        PilgrimPlayer player = new();
        Result hasStartedResult = await quests.HasStartedQuest(player, quest.Id);

        hasStartedResult.MustBeFailure();
    }
}
