using Avalonia;
using Avalonia.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public partial class TimeDateMenuItem : MenuItem
{
    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<TimeDateMenuItem, IAppTextBox?>(nameof(TextBox));

    public TimeDateMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}