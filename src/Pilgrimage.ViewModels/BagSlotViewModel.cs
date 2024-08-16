using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

public partial class BagSlotViewModel : ObservableObject<BagSlot>
{
    [ObservableProperty]
    ObservableCollection<ItemViewModel> _items = new();

    public BagSlotViewModel(BagSlot bagslot) : base(bagslot)
    {
        foreach (BagItem item in bagslot.Items)
        {
            ItemViewModel vm = new(item);
            _items.Add(vm);
        }
    }
}
