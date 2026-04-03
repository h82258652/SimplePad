using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Editor;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ZoomOutMenuFlyoutItem : MenuFlyoutItem
{
    private readonly CoreDispatcher _coreDispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomOutMenuFlyoutItem()
    {
        _coreDispatcher = Dispatcher;
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomOutChanged += OnEditorZoomStateCanZoomOutChanged;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomOut();
    }

    private async void OnEditorZoomStateCanZoomOutChanged(object? sender, bool e)
    {
        await _coreDispatcher.SafeRunAsync(UpdateIsEnabled);
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomOut;
    }
}
