using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Core.Extensions;

namespace SimplePad.Themes;

public partial class ThemeContainer : UserControl
{
    private readonly DispatcherQueue _dispatcherQueue;
    private readonly IThemeSettings _themeSettings;

    public ThemeContainer()
    {
        _dispatcherQueue = DispatcherQueue;
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        UpdateTheme();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private async void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        await _dispatcherQueue.SafeRunAsync(UpdateTheme);
    }

    private void UpdateTheme()
    {
        RequestedTheme = _themeSettings.AppTheme.GetElementTheme();
    }
}
