using System;
using System.Windows;

namespace SimplePad.Fonts;

public static class AppFontStyleWPFExtensions
{
    public static FontStyle GetWPFFontStyle(this AppFontStyle fontStyle)
    {
        if (fontStyle == AppFontStyle.Regular || fontStyle == AppFontStyle.Bold)
        {
            return FontStyles.Normal;
        }
        else if (fontStyle == AppFontStyle.Italic || fontStyle == AppFontStyle.BoldItalic)
        {
            return FontStyles.Italic;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }

    public static FontWeight GetWPFFontWeight(this AppFontStyle fontStyle)
    {
        if (fontStyle == AppFontStyle.Regular || fontStyle == AppFontStyle.Italic)
        {
            return FontWeights.Normal;
        }
        else if (fontStyle == AppFontStyle.Bold || fontStyle == AppFontStyle.BoldItalic)
        {
            return FontWeights.Bold;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(fontStyle));
        }
    }
}
