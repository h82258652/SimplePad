using System;
using System.Windows.Input;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed class GoToLineCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add
        {
        }
        remove
        {
        }
    }

    public IAppTextBox? TextBox { get; set; }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        if (TextBox is { } textBox)
        {
            await textBox.GoToLineAsync();
            textBox.Focus();
        }
    }
}