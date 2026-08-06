using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Editor;

public partial class IsSpellCheckEnabledSettingsControl : UserControl
{
    private readonly IEditorSettings _editorSettings;

    public IsSpellCheckEnabledSettingsControl()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private void OnIsSpellCheckEnabledToggleSwitchIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabled = IsSpellCheckEnabledToggleSwitch.IsChecked is true;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;

        UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabledChanged -= OnEditorSettingsIsSpellCheckEnabledChanged;
    }

    private void UpdateIsSpellCheckEnabledToggleSwitch()
    {
        IsSpellCheckEnabledToggleSwitch.IsChecked = _editorSettings.IsSpellCheckEnabled;
    }
}