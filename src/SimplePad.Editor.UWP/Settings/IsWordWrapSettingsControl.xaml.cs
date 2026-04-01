using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.Editor.Settings;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Editor.UWP.Settings;

public sealed partial class IsWordWrapSettingsControl : UserControl
{
    private readonly IEditorSettings _editorSettings;

    public IsWordWrapSettingsControl()
    {
        _editorSettings = ServiceLocator.Current.GetRequiredService<IEditorSettings>();

        InitializeComponent();

        UpdateToggleSwitch();

        _editorSettings.IsWordWrapChanged += OnEditorSettingsIsWordWrapChanged;
    }

    private async void OnEditorSettingsIsWordWrapChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateToggleSwitch);
    }

    private void UpdateToggleSwitch()
    {
        ToggleSwitch.IsOn = _editorSettings.IsWordWrap;
    }
}
