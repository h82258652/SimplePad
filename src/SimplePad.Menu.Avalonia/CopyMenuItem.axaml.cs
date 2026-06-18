using Avalonia;
using Avalonia.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class CopyMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<CopyMenuItem, IAppTextBox?>(nameof(TextBox));

    public CopyMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}