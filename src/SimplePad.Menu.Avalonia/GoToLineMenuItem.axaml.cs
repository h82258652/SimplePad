using Avalonia;
using Avalonia.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class GoToLineMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<GoToLineMenuItem, IAppTextBox?>(nameof(TextBox));

    public GoToLineMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}