using System;

namespace Pilgrimage;

public class PlayerQuestState
{
    public int Id { get; set; }

    public QuestState State { get; set; }

    public int NumTimesCompleted { get; set; }

    public DateTime StartTime { get; set; } = DateTime.Now;

    public DateTime? LastTimeCompleted { get; set; }
}
