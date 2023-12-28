using System.Windows;
using ColorNote.View;

namespace ColorNote.Controls;

public partial class NoteFieldsWindow : Window
{
    public MainWindow MainWindow { get; set; }
    public NoteFieldsWindow()
    {
        InitializeComponent();
    }
}