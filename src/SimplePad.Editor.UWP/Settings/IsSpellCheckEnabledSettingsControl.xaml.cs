using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Editor.UWP.Settings;

public sealed partial class IsSpellCheckEnabledSettingsControl : UserControl
{
    private readonly IEditorSettings _editorSettings;

    public IsSpellCheckEnabledSettingsControl()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsSpellCheckEnabledToggleSwitch();

        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;
    }

    private async void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateIsSpellCheckEnabledToggleSwitch);
    }

    private void OnIsSpellCheckEnabledToggleSwitchToggled(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabled = IsSpellCheckEnabledToggleSwitch.IsOn;
    }

    private void UpdateIsSpellCheckEnabledToggleSwitch()
    {
        IsSpellCheckEnabledToggleSwitch.IsOn = _editorSettings.IsSpellCheckEnabled;
    }
}
