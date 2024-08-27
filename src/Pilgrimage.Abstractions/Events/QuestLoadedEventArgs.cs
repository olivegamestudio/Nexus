using System;

namespace Pilgrimage;

/// <summary>  
/// Provides data for the QuestLoaded event.  
/// </summary>  
public class QuestLoadedEventArgs : EventArgs
{
    /// <summary>  
    /// Gets or sets the unique identifier of the loaded quest.  
    /// </summary>  
    public int QuestId { get; set; }
}
