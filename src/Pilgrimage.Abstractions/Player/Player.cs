using System.Collections.Generic;

namespace Pilgrimage;

public class Player
{
    public List<PlayerQuestState> Quests { get; set; } = new();

    public List<Bag> Inventory { get; set; } = new();

    public int Level { get; set; } = 1;
}
