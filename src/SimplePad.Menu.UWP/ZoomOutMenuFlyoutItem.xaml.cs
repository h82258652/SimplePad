using System.Threading.Tasks;
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
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(ZoomOutMenuFlyoutItem),
        null);

    private readonly CoreDispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomOutMenuFlyoutItem()
    {
        _dispatcher = Dispatcher;
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomOutChanged += OnEditorZoomStateCanZoomOutChanged;
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomOut();
        await Task.Yield();
        TextBox?.Focus();
    }

    private async void OnEditorZoomStateCanZoomOutChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateIsEnabled);
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomOut;
    }
}