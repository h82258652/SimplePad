using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SimplePad.Editor;

public sealed partial class IsWordWrapSettingsControl : UserControl
{
    private readonly Dispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;

    public IsWordWrapSettingsControl()
    {
        _dispatcher = Dispatcher;
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsWordWrapToggleSwitch();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
    }

    private void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsWordWrapToggleSwitch);
    }

    private void OnIsWordWrapToggleSwitchChecked(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = true;
    }

    private void OnIsWordWrapToggleSwitchUnchecked(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = false;
    }

    private void UpdateIsWordWrapToggleSwitch()
    {
        IsWordWrapToggleSwitch.IsChecked = _editorSettings.IsWordWrap;
    }
}