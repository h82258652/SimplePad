using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class ZoomOutMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(ZoomOutMenuItem),
        null);

    private readonly Dispatcher _dispatcher;
    private readonly EditorZoomState _editorZoomState;

    public ZoomOutMenuItem()
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

    private void OnEditorZoomStateCanZoomOutChanged(object? sender, bool e)
    {
        _dispatcher.Invoke(UpdateIsEnabled);
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomOut;
    }
}