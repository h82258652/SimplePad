using System;

namespace SimplePad.Themes.Settings;

public interface IThemeSettings
{
    AppTheme AppTheme { get; set; }

    event EventHandler<AppTheme>? AppThemeChanged;
}
