using System;
using Avalonia.Media;

namespace SimplePad.Fonts;

public static class AppFontStyleAvaloniaExtensions
{
    public static FontStyle GetAvaloniaFontStyle(this AppFontStyle fontStyle)
    {
        if (fontStyle == AppFontStyle.Regular || fontStyle == AppFontStyle.Bold)
        {
            return FontStyle.Normal;
        }
        else if (fontStyle == AppFontStyle.Italic || fontStyle == AppFontStyle.BoldItalic)
        {
            return FontStyle.Italic;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }

    public static FontWeight GetAvaloniaFontWeight(this AppFontStyle fontStyle)
    {
        if (fontStyle == AppFontStyle.Regular || fontStyle == AppFontStyle.Italic)
        {
            return FontWeight.Normal;
        }
        else if (fontStyle == AppFontStyle.Bold || fontStyle == AppFontStyle.BoldItalic)
        {
            return FontWeight.Bold;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }
}
