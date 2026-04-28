using System;
using System.Windows.Input;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed class DeleteCommand : ICommand
{
    private bool _executable;

    private IAppTextBox? _textBox;


    public IAppTextBox? TextBox
    {
        get => _textBox;
        set
        {
            if (_textBox == value)
            {
                return;
            }

            if (_textBox is { } oldTextBox)
            {
                oldTextBox.SelectionChanged -= OnTextBoxSelectionChanged;
            }

            if (value is { } newTextBox)
            {
                newTextBox.SelectionChanged += OnTextBoxSelectionChanged;
            }

            _textBox = value;

            UpdateExecutable();
        }
    }

    private bool Executable
    {
        get => _executable;
        set
        {
            if (_executable != value)
            {
                _executable = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void UpdateExecutable()
    {
        Executable = TextBox is { SelectionLength: > 0 };
    }

    private void OnTextBoxSelectionChanged(object? sender, EventArgs e)
    {
        UpdateExecutable();
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return Executable;
    }

    public void Execute(object? parameter)
    {
        if (_textBox is { } textBox)
        {
            textBox.SelectedText = string.Empty;
            textBox.Focus();
        }
    }
}