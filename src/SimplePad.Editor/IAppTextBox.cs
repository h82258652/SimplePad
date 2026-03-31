using System;

namespace SimplePad.Editor;

public interface IAppTextBox
{
    event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler<string>? TextChanged;

    event EventHandler? SelectionChanged;

    int SelectionLength { get; }

    CursorPosition CursorPosition { get; }

    string Text { get; }
}
