using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;

namespace Pilgrimage.Tool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[ObservableObject]
public partial class MainWindow : Window
{
    [ObservableProperty]
    ProjectViewModel _project = new();

    [ObservableProperty]
    QuestViewModel _selectedQuest;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = Project;
    }

    void FileOpen_OnClick(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog()
        {
            Filter = "JSON (*.json)|*.json",
            Multiselect=true,
        };

        if (dialog.ShowDialog() == true)
        {
            Project.Quests.Clear();

            foreach (string filename in dialog.FileNames)
            {
                string contents = File.ReadAllText(filename);
                Quest quest = JsonSerializer.Deserialize<Quest>(contents);
                Project.Quests.Add(new QuestViewModel(quest));
            }
        }
    }
}
