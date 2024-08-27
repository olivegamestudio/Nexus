using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Utility.Toolkit;

namespace Pilgrimage;

/// <summary>
/// ViewModel for a bag containing multiple slots of items.
/// </summary>
public partial class BagViewModel : ObservableObject<Bag>
{
    /// <summary>
    /// Gets or sets the maximum weight the bag can hold.
    /// </summary>
    [ObservableProperty]
    int _maxWeight;

    /// <summary>
    /// Updates the model when the maximum weight changes.
    /// </summary>
    /// <param name="value">The new maximum weight.</param>
    partial void OnMaxWeightChanged(int value)
    {
        Model.MaxWeight = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of slots the bag can have.
    /// </summary>
    [ObservableProperty]
    int _maxSlots;

    /// <summary>
    /// Updates the model when the maximum number of slots changes.
    /// </summary>
    /// <param name="value">The new maximum number of slots.</param>
    partial void OnMaxSlotsChanged(int value)
    {
        Model.MaxSlots = value;
    }

    /// <summary>
    /// Gets or sets the collection of bag slot view models.
    /// </summary>
    [ObservableProperty]
    ObservableCollection<BagSlotViewModel> _slots = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BagViewModel"/> class.
    /// </summary>
    /// <param name="bag">The bag model.</param>
    public BagViewModel(Bag bag) : base(bag)
    {
        MaxWeight = bag.MaxWeight;
        MaxSlots = bag.MaxSlots;
        RefreshSlots();
    }

    /// <summary>
    /// Refreshes the collection of bag slot view models.
    /// </summary>
    void RefreshSlots()
    {
        Slots.Clear();
        foreach (BagSlot slot in Model.Slots)
        {
            BagSlotViewModel vm = new(slot);
            Slots.Add(vm);
        }
    }
}
