using System;
using SimplePad.Core.Settings;

namespace SimplePad.Fonts.Settings;

public interface IFontSettings : IAppSettings
{
    event EventHandler<string>? FontFamilyChanged;

    event EventHandler<int>? FontSizeChanged;

    event EventHandler<AppFontStyle>? FontStyleChanged;

    string FontFamily { get; set; }

    int FontSize { get; set; }

    AppFontStyle FontStyle { get; set; }
}
