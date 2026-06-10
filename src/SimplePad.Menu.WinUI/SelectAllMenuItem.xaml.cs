using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class SelectAllMenuItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(SelectAllMenuItem),
        null);

    public SelectAllMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        TextBox?.SelectAll();
    }
}