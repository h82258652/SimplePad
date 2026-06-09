using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class PasteMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(PasteMenuItem),
        null);

    public PasteMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}