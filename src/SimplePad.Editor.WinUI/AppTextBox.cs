using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad.Editor;

public sealed class AppTextBox : TextBox, IAppTextBox
{
    public CursorPosition CursorPosition => throw new NotImplementedException();

    public event EventHandler<bool>? CanUndoChanged;
    public event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? IAppTextBox.SelectionChanged
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

    public void Focus()
    {
        Focus(FocusState.Programmatic);
    }

    public Task GoToLineAsync()
    {
        throw new NotImplementedException();
    }
}
