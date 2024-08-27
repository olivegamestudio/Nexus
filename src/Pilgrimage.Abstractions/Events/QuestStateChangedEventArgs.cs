using System;

namespace Pilgrimage;

/// <summary>
/// Provides data for the QuestStateChanged event.
/// </summary>
public class QuestStateChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the unique identifier of the quest.
    /// </summary>
    public int QuestId { get; set; }

    /// <summary>
    /// Gets or sets the state of the quest.
    /// </summary>
    public QuestState QuestState { get; set; }
}
