using System.Collections.Generic;
using LocalisedString;

namespace Pilgrimage;

/// <summary>  
/// Represents a quest with various properties and requirements.  
/// </summary>  
public class Quest
{
    /// <summary>  
    /// Gets or sets the unique identifier for the quest.  
    /// </summary>  
    public int Id { get; set; }

    /// <summary>  
    /// Gets or sets the minimum level required to start the quest.  
    /// </summary>  
    public int MinLevel { get; set; }

    /// <summary>  
    /// Gets or sets the starting position of the quest.  
    /// </summary>  
    public QuestPosition StartPosition { get; set; } = new();

    /// <summary>  
    /// Gets or sets the list of prerequisite quest IDs.  
    /// </summary>  
    public List<int> PreReqQuests { get; set; } = [];

    /// <summary>  
    /// Gets or sets the list of items required to complete the quest.  
    /// </summary>  
    public List<QuestRequiredItem> RequiredItems { get; set; } = new();

    /// <summary>  
    /// Gets or sets the list of items rewarded upon quest completion.  
    /// </summary>  
    public List<QuestRewardItem> RewardItems { get; set; } = new();

    /// <summary>  
    /// Gets or sets the localized title of the quest.  
    /// </summary>  
    public LocaleString Title { get; set; } = new();

    /// <summary>  
    /// Gets or sets the localized description of the quest.  
    /// </summary>  
    public LocaleString Description { get; set; } = new();

    /// <summary>  
    /// Gets or sets a value indicating whether the quest should be automatically shown.  
    /// </summary>  
    public bool AutoShow { get; set; }

    /// <summary>  
    /// Gets or sets the distance within which the quest should be automatically shown.  
    /// </summary>  
    public float AutoShowDistance { get; set; }
}
