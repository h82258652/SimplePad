namespace SimplePad.File;

public sealed record LineEndings
{
    public static readonly LineEndings CR = new("Macintosh (CR)", 1, "\r");
    public static readonly LineEndings CRLF = new("Windows (CRLF)", 0, "\r\n");
    public static readonly LineEndings LF = new("Unix (LF)", 2, "\n");

    private LineEndings(string name, int value, string newLine)
    {
        Name = name;
        Value = value;
        NewLine = newLine;
    }

    public string Name { get; }

    public string NewLine { get; }

    public int Value { get; }
}