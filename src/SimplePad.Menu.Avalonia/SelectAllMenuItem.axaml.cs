using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class SelectAllMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<SelectAllMenuItem, IAppTextBox?>(nameof(TextBox));

    public SelectAllMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object? sender, RoutedEventArgs e)
    {
        TextBox?.SelectAll();
    }
}