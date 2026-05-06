using System;

namespace SimplePad.Fonts;

public sealed record AppFontStyle
{
    public static readonly AppFontStyle Bold = new("Bold", 2);
    public static readonly AppFontStyle BoldItalic = new("Bold Italic", 3);
    public static readonly AppFontStyle Italic = new("Italic", 1);
    public static readonly AppFontStyle Regular = new("Regular", 0);

    private AppFontStyle(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }

    public int Value { get; }

    public static AppFontStyle FromValue(int value)
    {
        return value switch
        {
            0 => Regular,
            1 => Italic,
            2 => Bold,
            3 => BoldItalic,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
    }

    public override string ToString()
    {
        return Name;
    }
}