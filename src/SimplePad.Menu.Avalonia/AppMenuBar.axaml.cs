using Avalonia;
using SimplePad.Editor;
using SimplePad.Tabs;

namespace SimplePad.Menu;

public sealed partial class AppMenuBar : Avalonia.Controls.Menu
{
    public static readonly StyledProperty<Tab?> TabProperty =
        AvaloniaProperty.Register<AppMenuBar, Tab?>(nameof(Tab));

    public static readonly StyledProperty<IAppTextBox?> TextBoxProperty =
        AvaloniaProperty.Register<AppMenuBar, IAppTextBox?>(nameof(TextBox));

    public AppMenuBar()
    {
        InitializeComponent();
    }

    public Tab? Tab
    {
        get => GetValue(TabProperty);
        set => SetValue(TabProperty, value);
    }

    public IAppTextBox? TextBox
    {
        get => GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}