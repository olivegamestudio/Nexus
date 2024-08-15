using Musts;
using Utility;
using Pilgrimage;

namespace Pilgrimage.Tests;

public class QuestServiceTests
{
    [Test]
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

    [Test]
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

    [Test]
    public async Task Player_StartQuest_Then_HasStartedQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest quest = new() { Id = 1 };
        await quests.AddQuest(quest);

        Player player = new();

        Result startResult = await quests.StartQuest(player, quest.Id);
        Result result = await quests.HasStartedQuest(player, quest.Id);

        startResult.MustBeSuccess();
        result.MustBeSuccess();
    }

    [Test]
    public async Task Player_NotStartQuest_Then_HasNotStartedQuest()
    {
        IQuestService quests = new QuestService(new FileSystemFake());

        Quest quest = new() { Id = 1 };
        await quests.AddQuest(quest);

        Player player = new();
        Result result = await quests.HasStartedQuest(player, quest.Id);

        result.MustBeFailure();
    }
}
