using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;

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