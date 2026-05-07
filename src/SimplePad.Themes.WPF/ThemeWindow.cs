using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Windows;
using System.Windows.Threading;

namespace SimplePad.Themes;

public class ThemeWindow : Window
{
    private readonly IThemeSettings _themeSettings;
    private readonly Dispatcher _dispatcher;

    public ThemeWindow()
    {
        _dispatcher = Dispatcher;
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        UpdateTheme();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        _dispatcher.Invoke(UpdateTheme);
    }

    private void UpdateTheme()
    {
        ThemeMode = _themeSettings.AppTheme.GetThemeMode();
    }
}
