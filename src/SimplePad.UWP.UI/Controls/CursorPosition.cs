namespace SimplePad.UWP.UI.Controls;

public sealed class CursorPosition
{
    public CursorPosition(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Column { get; }

    public int Row { get; }
}
