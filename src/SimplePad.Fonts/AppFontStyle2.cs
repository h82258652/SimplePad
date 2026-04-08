using Ardalis.SmartEnum;

namespace SimplePad.Fonts;

public sealed class AppFontStyle2 : SmartEnum<AppFontStyle2>
{
    public static readonly AppFontStyle2 Regular = new("Regular", 0);
    public static readonly AppFontStyle2 Italic = new("Italic", 1);
    public static readonly AppFontStyle2 Bold = new("Bold", 2);
    public static readonly AppFontStyle2 BoldItalic = new("BoldItalic", 3);

    private AppFontStyle2(string name, int value) : base(name, value)
    {
    }
}
