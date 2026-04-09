using System;

namespace SimplePad.Editor;

public interface IAppTextBox
{
    event EventHandler<bool>? CanUndoChanged;

    event EventHandler<CursorPosition>? CursorPositionChanged;

    event EventHandler? SelectionChanged;

    event EventHandler<string>? TextChanged;

    bool CanUndo { get; }

    CursorPosition CursorPosition { get; }

    string SelectedText { get; set; }

    int SelectionLength { get; set; }

    int SelectionStart { get; set; }

    string Text { get; }

    void CopySelectionToClipboard();

    void CutSelectionToClipboard();

    void Focus();

    void GoToLine();

    void PasteFromClipboard();

    void SelectAll();

    void Undo();
}
