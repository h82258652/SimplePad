using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.Editor;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ZoomInMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(ZoomInMenuItem),
        null);

    private readonly CoreDispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomInMenuItem()
    {
        _dispatcher = Dispatcher;
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomInChanged += OnEditorZoomStateCanZoomInChanged;
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomIn();
        await Task.Yield();
        TextBox?.Focus();
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