using Ardalis.SmartEnum;

namespace SimplePad.File;

public sealed class LineEndings : SmartEnum<LineEndings>
{
    public static readonly LineEndings CR = new("CR", 1, "\r");
    public static readonly LineEndings CRLF = new("CRLF", 0, "\r\n");
    public static readonly LineEndings LF = new("LF", 2, "\n");

    private LineEndings(string name, int value, string newLine) : base(name, value)
    {
        NewLine = newLine;
    }

    public string NewLine { get; }
}
