using Windows.UI.Xaml;

namespace SimplePad.Themes;

public static class AppThemeUWPExtensions
{
    public static ElementTheme GetElementTheme(this AppTheme appTheme)
    {
        return appTheme switch
        {
            AppTheme.Light => ElementTheme.Light,
            AppTheme.Dark => ElementTheme.Dark,
            _ => ElementTheme.Default
        };
    }
}