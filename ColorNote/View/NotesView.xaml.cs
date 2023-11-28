using System;
using System.Windows;
using System.Windows.Controls;
using ColorNote.Data;
using ColorNote.ViewModel;

namespace ColorNote.View;

public partial class NotesView : UserControl
{
    private NotesViewModel _viewModel;

    public NotesView()
    {
        var date = DateTime.Now;
        
        InitializeComponent();
        _viewModel = new NotesViewModel(new NoteDataProvider());
        DataContext = _viewModel;
        Loaded += NotesView_Loaded;
    }

    private async void NotesView_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadAsync();
    }

}