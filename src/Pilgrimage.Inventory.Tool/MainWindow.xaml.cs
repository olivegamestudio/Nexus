using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Pilgrimage.Inventory.Tool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[ObservableObject]
public partial class MainWindow : Window
{
    [ObservableProperty]
    ObservableCollection<BagViewModel> _bags = new();

    [RelayCommand]
    void AddBag()
    {
        _player.Inventory.Add(new Bag());
        RefreshUI();
    }

    [RelayCommand]
    void CollectItem(string parameter)
    {
        _inventoryService.Collect(_player, new Item{Id=int.Parse(parameter)}, 1);
        RefreshUI();
    }

    readonly IInventoryService _inventoryService = new InventoryService();
    readonly Player _player = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        _inventoryService.InventoryChanged += InventoryServiceOnInventoryChanged;
    }

    void RefreshUI()
    {
        Bags.Clear();
        foreach (Bag bag in _player.Inventory)
        {
            Bags.Add(new BagViewModel(bag));
        }
    }

    void InventoryServiceOnInventoryChanged(object? sender, InventoryChangedEventArgs e)
    {
        RefreshUI();
    }
}
