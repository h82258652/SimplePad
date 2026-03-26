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
    public static readonly DependencyProperty SettingsViewModelProperty = DependencyProperty.Register(
        nameof(SettingsViewModel),
        typeof(SettingsViewModel),
        typeof(SettingsView),
        null);

    private readonly IAppSettings _appSettings;

    public SettingsView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();

        InitializeComponent();

        FontFamilyComboBox.ItemsSource = CanvasTextFormat.GetSystemFontFamilies();
    }

    public SettingsViewModel? ViewModel
    {
        get => (SettingsViewModel?)GetValue(SettingsViewModelProperty);
        set => SetValue(SettingsViewModelProperty, value);
    }

    private bool GetIsThemeEquals(AppTheme appTheme, AppTheme targetTheme)
    {
        return appTheme == targetTheme;
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ShellViewModel.IsSettingsViewVisible = false;
    }

    private void OnDarkThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Dark;
    }

    private void OnLightThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Light;
    }

    private void OnUseSystemSettingsThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.UseSystemSettings;
    }
     
    private void OnFontStyleComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (FontStyleComboBox.SelectedItem is AppFontStyle appFontStyle)
        {
            _appSettings.FontStyle = appFontStyle;
        }
    }
}
