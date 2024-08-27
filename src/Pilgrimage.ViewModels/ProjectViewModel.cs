using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Pilgrimage;

/// <summary>  
/// ViewModel for a project containing multiple quests.  
/// </summary>  
public partial class ProjectViewModel : ObservableObject
{
    /// <summary>  
    /// Gets or sets the collection of quest view models.  
    /// </summary>  
    [ObservableProperty]
    ObservableCollection<QuestViewModel> _quests = new();
}
