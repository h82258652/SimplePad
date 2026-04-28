namespace SimplePad.Editor;

public sealed class CursorPosition
{
    internal CursorPosition(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Column { get; }

    public int Row { get; }
}
