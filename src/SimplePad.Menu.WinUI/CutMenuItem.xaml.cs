using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class CutMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(CutMenuItem),
        null);

    public CutMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }
}
