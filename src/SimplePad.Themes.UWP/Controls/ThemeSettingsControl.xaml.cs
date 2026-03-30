using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Themes.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Themes.UWP.Controls;

public sealed partial class ThemeSettingsControl : UserControl
{
    private readonly IThemeSettings _themeSettings;

    public ThemeSettingsControl()
    {
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
        await Dispatcher.SafeRunAsync(UpdateRadioButtons);
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
