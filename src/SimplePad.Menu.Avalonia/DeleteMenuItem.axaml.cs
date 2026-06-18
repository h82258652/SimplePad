using Avalonia;
using Avalonia.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class DeleteMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<DeleteMenuItem, IAppTextBox?>(nameof(TextBox));

    public DeleteMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}