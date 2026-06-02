using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace SimplePad.Editor;

public partial class AppTextBox : TextBox, IAppTextBox
{
    public AppTextBox()
    {
        InitializeComponent();

        CanUndoProperty.Changed.Subscribe(null);
    }

    public CursorPosition CursorPosition => throw new NotImplementedException();

    public int SelectionLength
    {
        get => throw new NotImplementedException(); 
        set => throw new NotImplementedException(); 
    }

    public event EventHandler<bool>? CanUndoChanged;
    public event EventHandler<CursorPosition>? CursorPositionChanged;
    public event EventHandler? SelectionChanged;

    event EventHandler<string>? IAppTextBox.TextChanged
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }

    string IAppTextBox.Text
    {
        get
        {
            return Text ?? string.Empty;
        }
        set
        {
            Text = value;
        }
    }

    public void CopySelectionToClipboard()
    {
        Copy();
    }

    public void CutSelectionToClipboard()
    {
        Cut();
    }

    public void Focus()
    {
        base.Focus();
    }

    public Task GoToLineAsync()
    {
        throw new NotImplementedException();
    }

    public void PasteFromClipboard()
    {
        Paste();
    }
}