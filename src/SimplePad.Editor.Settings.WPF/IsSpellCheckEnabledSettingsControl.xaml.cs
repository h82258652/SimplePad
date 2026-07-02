using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimplePad.Editor;

public sealed partial class IsSpellCheckEnabledSettingsControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;

    public IsSpellCheckEnabledSettingsControl()
    {
        _dispatcher = Dispatcher;
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private void OnEditorSettingsIsSpellCheckEnabledChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsSpellCheckEnabledToggleSwitch);
    }

    private void OnIsSpellCheckEnabledToggleSwitchChecked(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabled = true;
    }

    private void OnIsSpellCheckEnabledToggleSwitchUnchecked(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabled = false;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabledChanged += OnEditorSettingsIsSpellCheckEnabledChanged;

        UpdateIsSpellCheckEnabledToggleSwitch();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsSpellCheckEnabledChanged -= OnEditorSettingsIsSpellCheckEnabledChanged;
    }

    private void UpdateIsSpellCheckEnabledToggleSwitch()
    {
        IsSpellCheckEnabledToggleSwitch.IsChecked = _editorSettings.IsSpellCheckEnabled;
    }
}