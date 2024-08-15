using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pilgrimage;

public partial class ProjectViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<QuestViewModel> _quests = new();
}
