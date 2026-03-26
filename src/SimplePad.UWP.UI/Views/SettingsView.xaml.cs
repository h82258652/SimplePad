using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class SettingsView : UserControl
{
    public static readonly DependencyProperty ShellViewModelProperty = DependencyProperty.Register(
        nameof(ShellViewModel),
        typeof(ShellViewModel),
        typeof(SettingsView),
        null);

    private readonly IAppSettings _appSettings;

    private bool GetIsThemeEquals(AppTheme appTheme, AppTheme targetTheme)
    {
        return appTheme == targetTheme;
    }

    public SettingsView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();

        InitializeComponent();

        FontFamilyComboBox.ItemsSource = CanvasTextFormat.GetSystemFontFamilies();
    }

    public ShellViewModel? ShellViewModel
    {
        get => (ShellViewModel?)GetValue(ShellViewModelProperty);
        set => SetValue(ShellViewModelProperty, value);
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        ShellViewModel?.IsSettingsViewVisible = false;
    }

    private void OnLightThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Light;
    }

    private void OnDarkThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Dark;
    }

    private void OnUseSystemSettingsThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.UseSystemSettings;
    }
}
