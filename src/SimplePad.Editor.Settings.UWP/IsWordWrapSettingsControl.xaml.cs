using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Editor;

public sealed partial class IsWordWrapSettingsControl : UserControl
{
    private readonly CoreDispatcher _dispatcher;
    private readonly IEditorSettings _editorSettings;

    public IsWordWrapSettingsControl()
    {
        _dispatcher = Dispatcher;
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateIsWordWrapToggleSwitch();
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateIsWordWrapToggleSwitch);
    }

    private void OnIsWordWrapToggleSwitchToggled(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrap = IsWordWrapToggleSwitch.IsOn;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;

        UpdateIsWordWrapToggleSwitch();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _editorSettings.IsWordWrapChanged -= OnEditorSettingsIsWordWrapChanged;
    }

    private void UpdateIsWordWrapToggleSwitch()
    {
        IsWordWrapToggleSwitch.IsOn = _editorSettings.IsWordWrap;
    }
}