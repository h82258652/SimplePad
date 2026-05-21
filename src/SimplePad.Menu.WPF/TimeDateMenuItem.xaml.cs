using System.Windows;
using System.Windows.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class TimeDateMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(TimeDateMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public TimeDateMenuItem()
    {
        InitializeComponent();
    }

    public IAppTextBox? TextBox
    {
        get => (IAppTextBox?)GetValue(TextBoxProperty);
        set => SetValue(TextBoxProperty, value);
    }

    private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TimeDateMenuItem self = (TimeDateMenuItem)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;
        self.TimeDateCommand.TextBox = textBox;
    }
}