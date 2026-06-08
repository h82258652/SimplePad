using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Editor;

public sealed partial class IsWordWrapSettingsControl : UserControl
{
    private readonly IEditorSettings _editorSettings;

    public IsWordWrapSettingsControl()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsWordWrapToggleSwitch();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
    }

    private void OnEditorSettingsIsWordWrapChanged(object sender, bool e)
    {
        UpdateIsWordWrapToggleSwitch();
    }

    private void OnIsWordWrapToggleSwitchToggled(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = IsWordWrapToggleSwitch.IsOn;
    }

    private void UpdateIsWordWrapToggleSwitch()
    {
        IsWordWrapToggleSwitch.IsOn = _editorSettings.IsWordWrap;
    }
}