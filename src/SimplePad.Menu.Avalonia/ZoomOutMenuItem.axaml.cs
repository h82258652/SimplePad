using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Editor;
using System;
using System.Threading.Tasks;

namespace SimplePad.Menu;

public partial class ZoomOutMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<ZoomOutMenuItem, IAppTextBox?>(nameof(TextBox));

    private readonly EditorZoomState _editorZoomState;

    public ZoomOutMenuItem()
    {
        _editorZoomState = ServiceLocator.Current.GetRequiredService<EditorZoomState>();

        InitializeComponent();

        UpdateIsEnabled();

        _editorZoomState.CanZoomOutChanged += OnEditorZoomStateCanZoomOutChanged;
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        _editorZoomState.ZoomOut();
        await Task.Yield();
        TextBox?.Focus();
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