using CommunityToolkit.Mvvm.ComponentModel;
using Utility.Toolkit;

namespace Pilgrimage;

public partial class ItemViewModel : ObservableObject<BagItem>
{
    [ObservableProperty]
    int _id;

    partial void OnIdChanged(int value)
    {
        Model.Id = value;
    }

    [ObservableProperty]
    int _count;

    partial void OnCountChanged(int value)
    {
        Model.Count = value;
    }

    [ObservableProperty]
    int _weight;

    partial void OnWeightChanged(int value)
    {
        Model.Weight = value;
    }

    public ItemViewModel(BagItem item) : base(item)
    {
        _id = Model.Id;
        _weight = Model.Weight;
        _count = Model.Count;
    }
}
