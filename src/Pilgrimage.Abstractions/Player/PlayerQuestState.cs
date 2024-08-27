using System;

namespace Pilgrimage;

/// <summary>
/// Represents the state of a player's quest.
/// </summary>
public class PlayerQuestState
{
    /// <summary>
    /// Gets or sets the unique identifier for the player's quest state.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the current state of the quest.
    /// </summary>
    public QuestState State { get; set; }

    /// <summary>
    /// Gets or sets the number of times the quest has been completed.
    /// </summary>
    public int NumTimesCompleted { get; set; }

    /// <summary>
    /// Gets or sets the start time of the quest.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the last time the quest was completed.
    /// </summary>
    public DateTime? LastTimeCompleted { get; set; }
}
