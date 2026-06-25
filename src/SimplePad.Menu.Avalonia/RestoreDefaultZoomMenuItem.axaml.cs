using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using System.Threading.Tasks;

namespace SimplePad.Menu;

public partial class RestoreDefaultZoomMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<RestoreDefaultZoomMenuItem, IAppTextBox?>(nameof(TextBox));

    private readonly EditorZoomState _editorZoomState;

    public RestoreDefaultZoomMenuItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        _editorZoomState.ResetZoomFactor();
        await Task.Yield();
        TextBox?.Focus();
    }
}