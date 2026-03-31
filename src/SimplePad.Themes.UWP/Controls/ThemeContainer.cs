using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Themes.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Themes.UWP.Controls;

public partial class ThemeContainer : UserControl
{
    private readonly IThemeSettings _themeSettings;

    public ThemeContainer()
    {
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        UpdateTheme();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private async void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        await Dispatcher.SafeRunAsync(UpdateTheme);
    }

    private void UpdateTheme()
    {
        RequestedTheme = _themeSettings.AppTheme switch
        {
            AppTheme.Light => ElementTheme.Light,
            AppTheme.Dark => ElementTheme.Dark,
            _ => ElementTheme.Default,
        };
    }
}
