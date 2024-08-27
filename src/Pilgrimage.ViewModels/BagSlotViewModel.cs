using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

/// <summary>  
/// ViewModel for a slot in a bag containing multiple items.  
/// </summary>  
public partial class BagSlotViewModel : ObservableObject<BagSlot>
{
    /// <summary>  
    /// Gets or sets the collection of item view models in the bag slot.  
    /// </summary>  
    [ObservableProperty]
    ObservableCollection<ItemViewModel> _items = new();

    /// <summary>  
    /// Initializes a new instance of the <see cref="BagSlotViewModel"/> class.  
    /// </summary>  
    /// <param name="bagslot">The bag slot model.</param>  
    public BagSlotViewModel(BagSlot bagslot) : base(bagslot)
    {
        foreach (BagItem item in bagslot.Items)
        {
            ItemViewModel vm = new(item);
            _items.Add(vm);
        }
    }
}
