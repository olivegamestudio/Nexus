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
