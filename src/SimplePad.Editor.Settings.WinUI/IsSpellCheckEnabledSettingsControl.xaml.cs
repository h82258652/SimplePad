using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Editor;

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

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object sender, bool e)
    {
        UpdateIsSpellCheckEnabledToggleSwitch();
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