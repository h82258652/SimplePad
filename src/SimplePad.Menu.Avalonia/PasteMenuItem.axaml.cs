using Avalonia;
using Avalonia.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class PasteMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<PasteMenuItem, IAppTextBox?>(nameof(TextBox));

    public PasteMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}