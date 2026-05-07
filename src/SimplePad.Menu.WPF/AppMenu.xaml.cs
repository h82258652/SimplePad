using SimplePad.Editor;
using System.Windows;

namespace SimplePad.Menu;

public partial class AppMenu : System.Windows.Controls.Menu
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(AppMenu),
        null);

    public AppMenu()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}
