using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using SparkIO;
using Utility;

namespace Pilgrimage;

public class QuestService : IQuestService
{
    readonly IFileSystem _fileSystem;
    readonly List<Quest> _quests = new();
    readonly ILogger<QuestService>? _logger;

    public QuestService(IFileSystem fileSystem, ILogger<QuestService> logger = null)
    {
        _fileSystem = fileSystem;
        _logger = logger;
    }

    /// <inheritdoc />
    public Result<IEnumerable<Quest>> GetAvailableQuests(Player player)
    {
        while (!HasLoaded)
        {
            Task.Yield();
        }

        List<Quest> quests = new();

        foreach (Quest quest in _quests)
        {
            if (player.IsEligibleToStartQuest(quest) && 
                player.HasCompletedPreRequistes(_quests, quest))
            {
                quests.Add(quest);
            }
        }

        return Result.Ok<IEnumerable<Quest>>(quests);
    }

    public Result<IEnumerable<Quest>> GetInProgressQuests(Player player)
    {
        while (!HasLoaded)
        {
            Task.Yield();
        }

        List<Quest> quests = new();

        foreach(PlayerQuestState state in player.Quests.Where(it=>it.State == QuestState.InProgress))
        {
            Quest quest = _quests.FirstOrDefault(it => it.Id == state.Id);
            if(quest is not null)
            {
                quests.Add(quest);
            }
        }

        return Result.Ok<IEnumerable<Quest>>(quests);
    }

    /// <inheritdoc />
    public IEnumerable<Quest> GetAllQuests() => _quests;

    public Task<Result<bool>> IsPreRequisite(int questId, int preReqQuestId)
    {
        if (!_quests.Any(it=>it.Id == questId))
        {
            return Task.FromResult(Result.Fail<bool>("The quest does not exist."));
        }

        Quest quest = _quests.First(it => it.Id == questId);
        return Task.FromResult(Result.Ok(quest.PreReqQuests.Contains(preReqQuestId)));
    }

    public Task<Result<Quest>> GetQuestForItem(int itemId)
    {
        foreach (Quest quest in GetAllQuests())
        {
            foreach (QuestRequiredItem requiredItem in quest.RequiredItems)
            {
                if (requiredItem.Id == itemId)
                {
                    return Task.FromResult(Result.Ok(quest));
                }
            }
        }

        return Task.FromResult(Result.Fail<Quest>("There are no quests that require this item."));
    }

    public Task<Result> IsItemRequiredByActiveQuest(Player player, int itemId)
    {
        foreach (PlayerQuestState questState in player.Quests)
        {
            if (questState.State == QuestState.InProgress)
            {
                Quest quest = GetAllQuests().FirstOrDefault(it => it.Id == questState.Id);
                if(quest is not null)
                {
                    foreach (QuestRequiredItem requiredItem in quest.RequiredItems)
                    {
                        if (requiredItem.Id == itemId)
                        {
                            return Task.FromResult(Result.Ok());
                        }
                    }
                }
            }
        }

        return Task.FromResult(Result.Fail("The item is not required by any in-progress quests."));
    }

    public Task<Result> CanCompleteQuest(Player player, int id)
    {
        while (!HasLoaded)
        {
            Task.Yield();
        }

        Quest quest = _quests.FirstOrDefault(it => it.Id == id);
        if (quest is null)
        {
            return Task.FromResult(Result.Fail($"The quest {id} was not found."));
        }

        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == id);
        if (state is null)
        {
            return Task.FromResult(Result.Fail($"The quest {id} has not been started."));
        }

        if (state.State == QuestState.Completed)
        {
            return Task.FromResult(Result.Fail($"The quest {id} has been completed and cannot be completed again."));
        }

        if (!player.HasCollectedItems(quest))
        {
            return Task.FromResult(Result.Fail("The player has not collected all of the required items to complete the quest."));
        }

        return Task.FromResult(Result.Ok());
    }

    public async Task<Result> CompleteQuest(Player player, IItemService items, IInventoryService inventory, int id)
    {
        while (!HasLoaded)
        {
            await Task.Yield();
        }

        Quest quest = _quests.FirstOrDefault(it => it.Id == id);
        if (quest is not null)
        {
            PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == id);
            if(state is null)
            {
                _logger?.LogWarning($"The quest {id} has not been started.");
                return Result.Fail($"The quest {id} has not been started.");
            }

            state.State = QuestState.Completed;
            state.LastTimeCompleted = DateTime.Now;
            state.NumTimesCompleted++;

            player.RemoveCollectedItems(quest);

            foreach (QuestRewardItem rewardItem in quest.RewardItems)
            {
                Result<Item> itemResult = await items.GetItem(rewardItem.Id);
                Result collectResult = await inventory.Collect(player, itemResult.Value, rewardItem.Count);
            }

            QuestStateChanged.Invoke(this,
                new QuestStateChangedEventArgs
                {
                    QuestId = id,
                    QuestState = state.State
                });

            _logger?.LogInformation($"The quest {id} has completed.");
            return Result.Ok();
        }

        _logger?.LogWarning($"The quest {id} cannot be completed as it doesn't exist.");
        return Result.Fail($"The quest {id} cannot be completed as it doesn't exist.");
    }

    public Task<Result> CanStartQuest(Player player, int id)
    {
        while (!HasLoaded)
        {
            Task.Yield();
        }

        Quest quest = _quests.FirstOrDefault(it => it.Id == id);
        if (quest is null)
        {           
            return Task.FromResult(Result.Fail($"The quest {id} does not exist."));
        }

        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == id);
        if(state is not null)
        {
            return Task.FromResult(Result.Fail($"The quest {id} is in progress or completed."));
        }

        foreach(int preReqQuest in quest.PreReqQuests)
        {
            PlayerQuestState preReqState = player.Quests.FirstOrDefault(it => it.Id == preReqQuest);
            if (preReqState is null)
            {
                return Task.FromResult(Result.Fail($"The quest {id} requires {preReqQuest} to be started and completed first."));
            }

            if(preReqState.State != QuestState.Completed)
            {
                return Task.FromResult(Result.Fail($"The quest {id} requires {preReqQuest} to be completed first."));
            }
        }

        return Task.FromResult(Result.Ok());
    }

    public Task<Result> StartQuest(Player player, int id)
    {
        while (!HasLoaded)
        {
            Task.Yield();
        }

        Quest quest = _quests.FirstOrDefault(it => it.Id == id);
        if (quest is not null)
        {
            if (player.Quests.Any(it => it.Id == id))
            {
                _logger?.LogWarning($"The quest {id} has already been started.");
                return Task.FromResult(Result.Fail($"The quest {id} has already been started."));
            }

            PlayerQuestState state = new PlayerQuestState { Id = id, State = QuestState.InProgress };
            player.Quests.Add(state);
            QuestStateChanged.Invoke(this,
                new QuestStateChangedEventArgs
                    {
                        QuestId = id, 
                        QuestState = state.State
                    });

            _logger?.LogInformation($"The quest {id} has started.");
            return Task.FromResult(Result.Ok());
        }

        _logger?.LogWarning($"The quest {id} cannot be started as it doesn't exist.");
        return Task.FromResult(Result.Fail($"The quest {id} cannot be started as it doesn't exist."));
    }

    public bool HasLoaded { get; set; }

    public async Task<Result> LoadQuests(IEnumerable<string> filenames)
    {
        foreach (string filename in filenames)
        {
            Result result = await LoadQuest(filename);
            if (result.IsFailure)
            {
                return result;
            }
        }

        HasLoaded = true;
        QuestsLoaded.Invoke(this, EventArgs.Empty);
        return Result.Ok();
    }

    public Task<Result> AddQuest(Quest quest)
    {
        _quests.Add(quest);
        HasLoaded = true;
        return Task.FromResult(Result.Ok());
    }

    /// <inheritdoc />
    public async Task<Result> LoadQuest(string filename)
    {
        Stream s = await _fileSystem.OpenStreamAsync(filename);
        Quest quest = await JsonSerializer.DeserializeAsync<Quest>(s, JsonSerializerOptions.Default);
        _quests.Add(quest);
        QuestLoaded.Invoke(this, new QuestLoadedEventArgs { QuestId = quest.Id });

        return Result.Ok();
    }

    public Task<Result> HasStartedQuest(Player player, int questId)
    {
        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == questId);
        if(state is null)
        {
            return Task.FromResult(Result.Fail("The quest has not been started."));
        }

        if (state.State == QuestState.InProgress)
        {
            return Task.FromResult(Result.Ok());
        }

        return Task.FromResult(Result.Fail("The quest is not in progress."));
    }

    public Task<Result> HasCompletedQuest(Player player, int questId)
    {
        PlayerQuestState state = player.Quests.FirstOrDefault(it => it.Id == questId);
        if (state is null)
        {
            return Task.FromResult(Result.Fail("The quest has not been started."));
        }

        if (state.State == QuestState.Completed)
        {
            return Task.FromResult(Result.Ok());
        }

        return Task.FromResult(Result.Fail("The quest has not been completed."));
    }

    /// <inheritdoc />
    public event EventHandler<QuestStateChangedEventArgs> QuestStateChanged = delegate { };

    /// <inheritdoc />
    public event EventHandler<QuestLoadedEventArgs> QuestLoaded = delegate { };

    /// <inheritdoc />
    public event EventHandler QuestsLoaded = delegate { };
}
