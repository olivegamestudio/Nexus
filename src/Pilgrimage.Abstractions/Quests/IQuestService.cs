
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace Pilgrimage;

/// <summary>  
/// Provides an interface for quest-related operations and events.  
/// </summary>  
public interface IQuestService
{
    /// <summary>  
    /// Loads quests from the specified filenames.  
    /// </summary>  
    /// <param name="filenames">The filenames of the quests to load.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the load operation.</returns>  
    Task<Result> LoadQuests(IEnumerable<string> filenames);

    /// <summary>  
    /// Loads a quest from the specified filename.  
    /// </summary>  
    /// <param name="filename">The filename of the quest to load.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the load operation.</returns>  
    Task<Result> LoadQuest(string filename);

    /// <summary>  
    /// Adds a new quest.  
    /// </summary>  
    /// <param name="quest">The quest to add.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the add operation.</returns>  
    Task<Result> AddQuest(Quest quest);

    /// <summary>  
    /// Gets a value indicating whether quests have been loaded.  
    /// </summary>  
    bool HasLoaded { get; }

    /// <summary>  
    /// Raised when a quest's state has changed, e.g., when a quest has been completed.  
    /// </summary>  
    event EventHandler<QuestStateChangedEventArgs> QuestStateChanged;

    /// <summary>  
    /// Raised when a quest has been loaded.  
    /// </summary>  
    event EventHandler<QuestLoadedEventArgs> QuestLoaded;

    /// <summary>  
    /// Raised when all quests have been loaded.  
    /// </summary>  
    event EventHandler QuestsLoaded;

    /// <summary>  
    /// Gets the list of available quests for the player that can be started.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <returns>Returns a list of available quests.</returns>  
    /// <remarks>  
    /// This will not return a quest that has been started.  
    /// This will return quests that have completed the pre-reqs; otherwise, they will be excluded.  
    /// </remarks>  
    Result<IEnumerable<Quest>> GetAvailableQuests(PilgrimPlayer player);

    /// <summary>  
    /// Gets the list of quests that are in progress for the player.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <returns>Returns a list of in-progress quests.</returns>  
    Result<IEnumerable<Quest>> GetInProgressQuests(PilgrimPlayer player);

    /// <summary>  
    /// Gets the entire list of quests.  
    /// </summary>  
    /// <returns>Returns a list of all the loaded quests.</returns>  
    IEnumerable<Quest> GetAllQuests();

    /// <summary>  
    /// Determines whether a quest is a prerequisite for another quest.  
    /// </summary>  
    /// <param name="questId">The quest ID.</param>  
    /// <param name="preReqQuestId">The prerequisite quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result<bool>> IsPreRequisite(int questId, int preReqQuestId);

    /// <summary>  
    /// Determines whether an item is required by an active quest for the player.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="itemId">The item ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result> IsItemRequiredByActiveQuest(PilgrimPlayer player, int itemId);

    /// <summary>  
    /// Gets the quest associated with a specific item.  
    /// </summary>  
    /// <param name="itemId">The item ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the quest associated with the item.</returns>  
    Task<Result<Quest>> GetQuestForItem(int itemId);

    /// <summary>  
    /// Determines whether the player has started a specific quest.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="questId">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result> HasStartedQuest(PilgrimPlayer player, int questId);

    /// <summary>  
    /// Determines whether the player can start a specific quest.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="questId">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result> CanStartQuest(PilgrimPlayer player, int questId);

    /// <summary>  
    /// Determines whether the player can complete a specific quest.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="questId">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result> CanCompleteQuest(PilgrimPlayer player, int questId);

    /// <summary>  
    /// Determines whether the player has completed a specific quest.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="questId">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the check.</returns>  
    Task<Result> HasCompletedQuest(PilgrimPlayer player, int questId);

    /// <summary>  
    /// Starts a specific quest for the player.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="id">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the start operation.</returns>  
    Task<Result> StartQuest(PilgrimPlayer player, int id);

    /// <summary>  
    /// Completes a specific quest for the player.  
    /// </summary>  
    /// <param name="player">The player.</param>  
    /// <param name="items">The item service.</param>  
    /// <param name="inventory">The inventory service.</param>  
    /// <param name="id">The quest ID.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the complete operation.</returns>  
    Task<Result> CompleteQuest(PilgrimPlayer player, IItemService items, IInventoryService inventory, int id);
}
