using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class TimeDateMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(TimeDateMenuItem),
        null);

    public TimeDateMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}
