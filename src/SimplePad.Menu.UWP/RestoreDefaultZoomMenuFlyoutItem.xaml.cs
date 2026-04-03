using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class RestoreDefaultZoomMenuFlyoutItem : MenuFlyoutItem
{
    private readonly EditorZoomState _editorZoomState;

    public RestoreDefaultZoomMenuFlyoutItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ResetZoomFactor();
    }
}
