using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class GoToLineMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(GoToLineMenuFlyoutItem),
        null);

    public GoToLineMenuFlyoutItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (TextBox is { } textBox)
        {
            await textBox.GoToLineAsync();
            textBox.Focus();
        }
    }
}