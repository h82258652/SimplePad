using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class ZoomOutMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(ZoomOutMenuItem),
        null);

    private readonly EditorZoomState _editorZoomState;

    public ZoomOutMenuItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomOutChanged += OnEditorZoomStateCanZoomOutChanged;
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomOut();
        await Task.Yield();
        TextBox?.Focus();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnEditorZoomStateCanZoomOutChanged(object? sender, bool e)
    {
        UpdateIsEnabled();
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomOut;
    }
}