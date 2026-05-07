using System;

namespace SimplePad.Tabs;

public sealed record OpenFileBehavior
{
    public static readonly OpenFileBehavior NewTab = new("Open in a new tab", 0);
    public static readonly OpenFileBehavior NewWindow = new("Open in a new window", 1);

    private OpenFileBehavior(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }

    public int Value { get; }

    public static OpenFileBehavior FromValue(int value)
    {
        return value switch
        {
            0 => NewTab,
            1 => NewWindow,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
    }

    public override string ToString()
    {
        return Name;
    }
}