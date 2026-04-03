using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Editor;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ZoomInMenuFlyoutItem : MenuFlyoutItem
{
    private readonly CoreDispatcher _coreDispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomInMenuFlyoutItem()
    {
        _coreDispatcher = Dispatcher;
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomInChanged += OnEditorZoomStateCanZoomInChanged;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomIn();
    }

    private async void OnEditorZoomStateCanZoomInChanged(object? sender, bool e)
    {
        await _coreDispatcher.SafeRunAsync(UpdateIsEnabled);
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomIn;
    }
}
