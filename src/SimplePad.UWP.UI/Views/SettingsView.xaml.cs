using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor.Settings;
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

    private readonly IEditorSettings _editorSettings;

    public SettingsView()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        _editorSettings.IsSpellCheckEnabledChanged += _editorSettings_IsSpellCheckEnabledChanged;

        _ = UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private async void _editorSettings_IsSpellCheckEnabledChanged(object? sender, bool e)
    {
        await UpdateIsSpellCheckEnabledToggleSwitch();
    }

    public SettingsViewModel? ViewModel
    {
        get => (SettingsViewModel?)GetValue(SettingsViewModelProperty);
        set => SetValue(SettingsViewModelProperty, value);
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
            IsSpellCheckEnabledToggleSwitch.IsOn = _editorSettings.IsSpellCheckEnabled;
        });
    }
}