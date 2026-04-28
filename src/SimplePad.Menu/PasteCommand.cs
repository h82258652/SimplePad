using System;
using System.Windows.Input;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed class PasteCommand : ICommand
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

    public void Execute(object? parameter)
    {
        if (TextBox is { } textBox)
        {
            textBox.PasteFromClipboard();
            textBox.Focus();
        }
    }
}