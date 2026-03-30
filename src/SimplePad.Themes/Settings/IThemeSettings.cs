using System;
using SimplePad.Core.Settings;

namespace SimplePad.Themes.Settings;

public interface IThemeSettings : IAppSettings
{
    AppTheme AppTheme { get; set; }

    event EventHandler<AppTheme>? AppThemeChanged;
}
