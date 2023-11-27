using System;
using System.Windows.Input;

namespace ColorNote.Command;

public class DelegateCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool CanExecute(object? parameter)
    {
        return _canExecute is null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}