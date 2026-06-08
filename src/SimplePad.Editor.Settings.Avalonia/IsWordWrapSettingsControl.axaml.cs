using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
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

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        UpdateIsWordWrapToggleSwitch();
    }

    private void OnIsWordWrapToggleSwitchIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = IsWordWrapToggleSwitch.IsChecked is true;
    }

    private void UpdateIsWordWrapToggleSwitch()
    {
        IsWordWrapToggleSwitch.IsChecked = _editorSettings.IsWordWrap;
    }
}