using System;
using Windows.UI.Text;

namespace SimplePad.Fonts;

public static class AppFontStyleUWPExtensions
{
    public static FontStyle GetUWPFontStyle(this AppFontStyle fontStyle)
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

    public static FontWeight GetUWPFontWeight(this AppFontStyle fontStyle)
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
