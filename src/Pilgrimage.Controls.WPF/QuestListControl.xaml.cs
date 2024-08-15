using System.Windows;
using System.Windows.Controls;

namespace Pilgrimage.Controls.WPF
{
    public partial class QuestListControl : UserControl
    {
        public QuestListControl()
        {
            InitializeComponent();
        }

        void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
        }
    }
}
