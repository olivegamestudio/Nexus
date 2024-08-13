using System;

namespace Pilgrimage;

public class QuestStateChangedEventArgs : EventArgs
{
    public int QuestId { get; set; }

    public QuestState QuestState { get; set; }
}
