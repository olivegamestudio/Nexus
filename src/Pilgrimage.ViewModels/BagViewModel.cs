using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Utility.Toolkit;

namespace Pilgrimage;

public partial class BagViewModel : ObservableObject<Bag>
{
    [ObservableProperty]
    int _maxWeight;

    partial void OnMaxWeightChanged(int value)
    {
        Model.MaxWeight = value;
    }

    [ObservableProperty]
    int _maxSlots;

    partial void OnMaxSlotsChanged(int value)
    {
        Model.MaxSlots = value;
    }

    [ObservableProperty]
    ObservableCollection<BagSlotViewModel> _slots = new();

    public BagViewModel(Bag bag) : base(bag)
    {
        MaxWeight = bag.MaxWeight;
        MaxSlots = bag.MaxSlots;
        RefreshSlots();
    }

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
