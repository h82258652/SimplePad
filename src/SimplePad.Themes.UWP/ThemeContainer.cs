using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Themes;

public partial class ThemeContainer : UserControl
{
    private readonly CoreDispatcher _dispatcher;
    private readonly IThemeSettings _themeSettings;

    public ThemeContainer()
    {
        _dispatcher = Dispatcher;
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        UpdateTheme();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private async void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        await _dispatcher.SafeRunAsync(UpdateTheme);
    }

    private void UpdateTheme()
    {
        RequestedTheme = _themeSettings.AppTheme.GetElementTheme();
    }
}