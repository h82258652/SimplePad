using System;
using Windows.UI.Text;

namespace SimplePad.Fonts.UWP.Extensions;

public static class AppFontStyleExtensions
{
    public static FontStyle GetUWPFontStyle(this AppFontStyle fontStyle)
    {
        return fontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Bold => FontStyle.Normal,
            AppFontStyle.Italic or AppFontStyle.BoldItalic => FontStyle.Italic,
            _ => throw new ArgumentOutOfRangeException(nameof(fontStyle)),
        };
    }

    public static FontWeight GetUWPFontWeight(this AppFontStyle fontStyle)
    {
        return fontStyle switch
        {
            AppFontStyle.Regular or AppFontStyle.Italic => FontWeights.Normal,
            AppFontStyle.Bold or AppFontStyle.BoldItalic => FontWeights.Bold,
            _ => throw new ArgumentOutOfRangeException(nameof(fontStyle)),
        };
    }
}
