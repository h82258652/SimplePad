using System.Windows;
using System.Windows.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class GoToLineMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(GoToLineMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public GoToLineMenuItem()
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
        GoToLineMenuItem self = (GoToLineMenuItem)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;
        self.GoToLineCommand.TextBox = textBox;
    }
}