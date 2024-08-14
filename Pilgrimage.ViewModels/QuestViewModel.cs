using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

public partial class QuestViewModel : ObservableObject<Quest>
{
    public QuestViewModel(Quest quest) : base(quest)
    {
        _id = Model.Id;
        _minLevel = Model.MinLevel;
        _autoShow = Model.AutoShow;
    }

    [ObservableProperty]
    int _id;

    [ObservableProperty]
    int _minLevel;

    [ObservableProperty]
    bool _autoShow;
}

/*
 *    public int Id { get; set; }

   public int MinLevel { get; set; }

   public QuestPosition StartPosition { get; set; } = new();

   public List<int> PreReqQuests { get; set; } = [];

   public List<QuestRequiredItem> RequiredItems { get; set; } = new();

   public LocaleString Title { get; set; } = new();

   public LocaleString Description { get; set; } = new();

   public bool AutoShow { get; set; }

   public float AutoShowDistance { get; set; }

 */