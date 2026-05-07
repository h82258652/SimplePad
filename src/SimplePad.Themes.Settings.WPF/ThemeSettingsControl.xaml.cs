using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimplePad.Themes;

public partial class ThemeSettingsControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly IThemeSettings _themeSettings;

    public ThemeSettingsControl()
    {
        _dispatcher = Dispatcher;
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();

        InitializeComponent();

        UpdateRadioButtons();

        _themeSettings.AppThemeChanged += OnThemeSettingsAppThemeChanged;
    }

    private void OnDarkThemeRadioButtonChecked(object sender, System.Windows.RoutedEventArgs e)
    {
        _themeSettings.AppTheme = AppTheme.Dark;
    }

    private void OnLightThemeRadioButtonChecked(object sender, System.Windows.RoutedEventArgs e)
    {
        _themeSettings.AppTheme = AppTheme.Light;
    }

    private void OnThemeSettingsAppThemeChanged(object? sender, AppTheme e)
    {
        _dispatcher.Invoke(UpdateRadioButtons);
    }

    private void OnUseSystemSettingsThemeRadioButtonChecked(object sender, System.Windows.RoutedEventArgs e)
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