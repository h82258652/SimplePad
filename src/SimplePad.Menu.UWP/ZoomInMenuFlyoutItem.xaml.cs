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
    private readonly CoreDispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomInMenuFlyoutItem()
    {
        _dispatcher = Dispatcher;
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
        await _dispatcher.SafeRunAsync(UpdateIsEnabled);
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomIn;
    }
}
