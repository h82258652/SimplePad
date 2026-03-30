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

        _appSettings.PropertyChanged += OnAppSettingsPropertyChanged;
         
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
        if (e.PropertyName == nameof(_appSettings.IsWordWrap))
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
}
