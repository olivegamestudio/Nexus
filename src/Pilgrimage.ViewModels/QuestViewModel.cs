using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

/// <summary>
/// ViewModel for a quest.
/// </summary>
public partial class QuestViewModel : ObservableObject<Quest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestViewModel"/> class.
    /// </summary>
    /// <param name="quest">The quest model.</param>
    public QuestViewModel(Quest quest) : base(quest)
    {
        _id = Model.Id;
        _minLevel = Model.MinLevel;
        _autoShow = Model.AutoShow;
    }

    /// <summary>
    /// Gets or sets the quest ID.
    /// </summary>
    [ObservableProperty]
    int _id;

    /// <summary>
    /// Gets or sets the minimum level required for the quest.
    /// </summary>
    [ObservableProperty]
    int _minLevel;

    /// <summary>
    /// Gets or sets a value indicating whether the quest should be automatically shown.
    /// </summary>
    [ObservableProperty]
    bool _autoShow;
}
