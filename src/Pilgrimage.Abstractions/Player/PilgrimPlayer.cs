using System.Collections.Generic;

namespace Pilgrimage;

public class PilgrimPlayer
{
    public List<PlayerQuestState> Quests { get; set; } = new();

    public List<Bag> Inventory { get; set; } = new();

    public int Level { get; set; } = 1;
}
