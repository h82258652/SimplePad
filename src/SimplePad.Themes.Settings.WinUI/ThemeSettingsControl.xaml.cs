using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Core.Extensions;

namespace SimplePad.Themes;

public sealed partial class ThemeSettingsControl : UserControl
{
    private readonly DispatcherQueue _dispatcherQueue;
    private readonly IThemeSettings _themeSettings;

    public ThemeSettingsControl()
    {
        _dispatcherQueue = DispatcherQueue;
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        InitializeComponent();

        UpdateRadioButtons();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private void OnDarkThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _themeSettings.AppTheme = AppTheme.Dark;
    }

    private void OnLightThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _themeSettings.AppTheme = AppTheme.Light;
    }

    private async void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        await _dispatcherQueue.SafeRunAsync(UpdateRadioButtons);
    }

    private void OnUseSystemSettingsThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _themeSettings.AppTheme = AppTheme.Default;
    }

    private void UpdateRadioButtons()
    {
        switch (_themeSettings.AppTheme)
        {
            case AppTheme.Light:
                LightThemeRadioButton.IsChecked = true;
                break;

            case AppTheme.Dark:
                DarkThemeRadioButton.IsChecked = true;
                break;

            default:
                UseSystemSettingsThemeRadioButton.IsChecked = true;
                break;
        }
    }
}