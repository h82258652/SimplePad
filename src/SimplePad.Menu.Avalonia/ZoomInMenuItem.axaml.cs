using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using System.Threading.Tasks;

namespace SimplePad.Menu;

public partial class ZoomInMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<ZoomInMenuItem, IAppTextBox?>(nameof(TextBox));

    private readonly EditorZoomState _editorZoomState;

    public ZoomInMenuItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomInChanged += OnEditorZoomStateCanZoomInChanged;
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomIn();
        await Task.Yield();
        TextBox?.Focus();
    }

    private void OnEditorZoomStateCanZoomInChanged(object? sender, bool e)
    {
        UpdateIsEnabled();
    }

    private void UpdateIsEnabled()
    {
        IsEnabled = _editorZoomState.CanZoomIn;
    }
}