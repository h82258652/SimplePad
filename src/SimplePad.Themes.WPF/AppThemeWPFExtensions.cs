using System.Windows;

namespace SimplePad.Themes;

public static class AppThemeWPFExtensions
{
    public static ThemeMode GetThemeMode(this AppTheme appTheme)
    {
        return appTheme switch
        {
            AppTheme.Light => ThemeMode.Light,
            AppTheme.Dark => ThemeMode.Dark,
            _ => ThemeMode.System
        };
    }
}
