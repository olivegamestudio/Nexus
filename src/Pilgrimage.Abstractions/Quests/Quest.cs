using System.Collections.Generic;

namespace Pilgrimage;

public class Quest
{
    public int Id { get; set; }

    public int MinLevel { get; set; }

    public QuestPosition StartPosition { get; set; } = new();

    public List<int> PreReqQuests { get; set; } = [];

    public List<QuestRequiredItem> RequiredItems { get; set; } = new();

    public QuestText Title { get; set; } = new();

    public QuestText Description { get; set; } = new();

    public bool AutoShow { get; set; }

    public float AutoShowDistance { get; set; }
}
