using System;

namespace Pilgrimage;

public class QuestLoadedEventArgs : EventArgs
{
    public int QuestId { get; set; }
}
