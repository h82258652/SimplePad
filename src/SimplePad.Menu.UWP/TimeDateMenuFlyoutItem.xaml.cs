using System;
using SimplePad.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class TimeDateMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(TimeDateMenuFlyoutItem),
        null);

    public TimeDateMenuFlyoutItem()
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
        if (TextBox is not { } textBox)
        {
            return;
        }

        string timeDateText = DateTime.Now.ToString("HH:mm yyyy/M/dd");
        textBox.SelectedText = timeDateText;
        textBox.SelectionLength = 0;
        textBox.SelectionStart += timeDateText.Length;
    }
}
