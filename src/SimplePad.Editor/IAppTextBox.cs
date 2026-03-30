using System;

namespace SimplePad.Editor;

public interface IAppTextBox
{
    CursorPosition CursorPosition { get; }

    event EventHandler<CursorPosition>? CursorPositionChanged;
}
