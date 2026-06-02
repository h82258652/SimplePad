using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Themes;

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

    private void OnDarkThemeRadioButtonIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (DarkThemeRadioButton.IsChecked is true)
        {
            _themeSettings.AppTheme = AppTheme.Dark;
        }
    }

    private void OnLightThemeRadioButtonIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (LightThemeRadioButton.IsChecked is true)
        {
            _themeSettings.AppTheme = AppTheme.Light;
        }
    }

    private void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        UpdateRadioButtons();
    }

    private void OnUseSystemSettingsThemeRadioButtonIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (UseSystemSettingsThemeRadioButton.IsChecked is true)
        {
            _themeSettings.AppTheme = AppTheme.Default;
        }
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