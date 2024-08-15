using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilgrimage.Controls.WPF;

/// <summary>
/// Interaction logic for QuestsCanvas.xaml
/// </summary>
public partial class QuestsCanvas : UserControl
{
    public QuestsCanvas()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty QuestsProperty =
        DependencyProperty.Register(
            name: "Quests",
            propertyType: typeof(ObservableCollection<QuestViewModel>),
            ownerType: typeof(QuestsCanvas),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: null, OnQuestsChanged));

    static void OnQuestsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ObservableCollection<QuestViewModel> quests = e.NewValue as ObservableCollection<QuestViewModel>;
        quests.CollectionChanged += OnCollectionChanged;
    }

    static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        
    }

    public ObservableCollection<QuestViewModel> Quests
    {
        get => (ObservableCollection<QuestViewModel>)GetValue(QuestsProperty);
        set => SetValue(QuestsProperty, value);
    }
}
