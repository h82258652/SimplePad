using System;
using System.Windows.Input;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed class TimeDateCommand : ICommand
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
        if (TextBox is not { } textBox)
        {
            return;
        }

        string timeDateText = DateTime.Now.ToString("HH:mm yyyy/M/dd");
        textBox.SelectedText = timeDateText;
        textBox.SelectionLength = 0;
        textBox.SelectionStart += timeDateText.Length;
        textBox.Focus();
    }
}