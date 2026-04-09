using Ardalis.SmartEnum;

namespace SimplePad.Fonts;

public sealed class AppFontStyle : SmartEnum<AppFontStyle>
{
    public static readonly AppFontStyle Regular = new("Regular", 0);
    public static readonly AppFontStyle Italic = new("Italic", 1);
    public static readonly AppFontStyle Bold = new("Bold", 2);
    public static readonly AppFontStyle BoldItalic = new("Bold Italic", 3);

    private AppFontStyle(string name, int value) : base(name, value)
    {
    }
}
