using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas.Text;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.UWP.UI.Extensions;
using SimplePad.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.UWP.UI.Views;

public sealed partial class SettingsView : UserControl
{
    public static readonly DependencyProperty SettingsViewModelProperty =
        DependencyProperty.Register(
            nameof(SettingsViewModel),
            typeof(SettingsViewModel),
            typeof(SettingsView),
            null
        );

    private readonly IAppSettings _appSettings;

    public SettingsView()
    {
        _appSettings = ServiceLocator.Current.GetRequiredService<IAppSettings>();

        InitializeComponent();
        FontFamilyComboBox.ItemsSource = CanvasTextFormat.GetSystemFontFamilies();

        _appSettings.PropertyChanged += OnAppSettingsPropertyChanged;

        _ = UpdateThemeRadioButtons();
        _ = UpdateFontFamilyComboBox();
        _ = UpdateFontStyleComboBox();
        _ = UpdateFontSizeComboBox();
        _ = UpdateIsWordWrapToggleSwitch();
        _ = UpdateOpenFileBehaviorComboBox();
        _ = UpdateIsSpellCheckEnabledToggleSwitch();
    }

    public SettingsViewModel? ViewModel
    {
        get => (SettingsViewModel?)GetValue(SettingsViewModelProperty);
        set => SetValue(SettingsViewModelProperty, value);
    }

    private async void OnAppSettingsPropertyChanged(
        object? sender,
        System.ComponentModel.PropertyChangedEventArgs e
    )
    {
        if (e.PropertyName == nameof(_appSettings.AppTheme))
        {
            await UpdateThemeRadioButtons();
        }
        else if (e.PropertyName == nameof(_appSettings.FontFamily))
        {
            // TODO
        }
        else if (e.PropertyName == nameof(_appSettings.FontStyle))
        {
            await UpdateFontStyleComboBox();
        }
        else if (e.PropertyName == nameof(_appSettings.IsWordWrap))
        {
            await UpdateIsWordWrapToggleSwitch();
        }
        else if (e.PropertyName == nameof(_appSettings.IsSpellCheckEnabled))
        {
            await UpdateIsSpellCheckEnabledToggleSwitch();
        }
    }

    private void OnBackButtonClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel is { ShellViewModel: { } shellViewModel })
        {
            shellViewModel.IsSettingsViewVisible = false;
        }
    }

    private async void OnDarkThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Dark;
        await _appSettings.SaveAsync();
    }

    private async void OnFontStyleComboBoxSelectionChanged(
        object sender,
        SelectionChangedEventArgs e
    )
    {
        if (FontStyleComboBox.SelectedItem is AppFontStyle appFontStyle)
        {
            _appSettings.FontStyle = appFontStyle;
            await _appSettings.SaveAsync();
        }
    }

    private async void OnLightThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.Light;
        await _appSettings.SaveAsync();
    }

    private async void OnUseSystemSettingsThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        _appSettings.AppTheme = AppTheme.UseSystemSettings;
        await _appSettings.SaveAsync();
    }

    private Task UpdateFontFamilyComboBox()
    {
        return Dispatcher.SafeRunAsync(() => {
            // TODO
        });
    }

    private Task UpdateFontSizeComboBox()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            FontSizeComboBox.Text = _appSettings.FontSize.ToString();
        });
    }

    private Task UpdateFontStyleComboBox()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            FontStyleComboBox.SelectedItem = _appSettings.FontStyle;
        });
    }

    private Task UpdateIsSpellCheckEnabledToggleSwitch()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            IsSpellCheckEnabledToggleSwitch.IsOn = _appSettings.IsSpellCheckEnabled;
        });
    }

    private Task UpdateIsWordWrapToggleSwitch()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            IsWordWrapToggleSwitch.IsOn = _appSettings.IsWordWrap;
        });
    }

    private Task UpdateOpenFileBehaviorComboBox()
    {
        return Dispatcher.SafeRunAsync(() => {
            // TODO
        });
    }

    private Task UpdateThemeRadioButtons()
    {
        return Dispatcher.SafeRunAsync(() =>
        {
            switch (_appSettings.AppTheme)
            {
                case AppTheme.UseSystemSettings:
                    UseSystemSettingsThemeRadioButton.IsChecked = true;
                    break;

                case AppTheme.Light:
                    LightThemeRadioButton.IsChecked = true;
                    break;

                case AppTheme.Dark:
                    DarkThemeRadioButton.IsChecked = true;
                    break;
            }
        });
    }
}
