using System.Collections.Generic;

namespace Pilgrimage;

/// <summary>
/// Represents a player in the game.
/// </summary>
public class PilgrimPlayer
{
    /// <summary>
    /// Gets or sets the list of quests associated with the player.
    /// </summary>
    public List<PlayerQuestState> Quests { get; set; } = new();

    /// <summary>
    /// Gets or sets the inventory of the player.
    /// </summary>
    public List<Bag> Inventory { get; set; } = new();

    /// <summary>
    /// Gets or sets the level of the player.
    /// </summary>
    public int Level { get; set; } = 1;
}
