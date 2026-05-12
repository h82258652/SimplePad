using SimplePad.Editor;
using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Menu;

public partial class SelectAllMenuItem : MenuItem
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