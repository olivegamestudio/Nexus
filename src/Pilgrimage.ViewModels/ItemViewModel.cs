using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

/// <summary>
/// ViewModel for an item in a bag.
/// </summary>
public partial class ItemViewModel : ObservableObject<BagItem>
{
    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    [ObservableProperty]
    int _id;

    /// <summary>
    /// Updates the model when the item ID changes.
    /// </summary>
    /// <param name="value">The new item ID.</param>
    partial void OnIdChanged(int value)
    {
        Model.Id = value;
    }

    /// <summary>
    /// Gets or sets the item count.
    /// </summary>
    [ObservableProperty]
    int _count;

    /// <summary>
    /// Updates the model when the item count changes.
    /// </summary>
    /// <param name="value">The new item count.</param>
    partial void OnCountChanged(int value)
    {
        Model.Count = value;
    }

    /// <summary>
    /// Gets or sets the item weight.
    /// </summary>
    [ObservableProperty]
    int _weight;

    /// <summary>
    /// Updates the model when the item weight changes.
    /// </summary>
    /// <param name="value">The new item weight.</param>
    partial void OnWeightChanged(int value)
    {
        Model.Weight = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemViewModel"/> class.
    /// </summary>
    /// <param name="item">The bag item model.</param>
    public ItemViewModel(BagItem item) : base(item)
    {
        _id = Model.Id;
        _weight = Model.Weight;
        _count = Model.Count;
    }
}
