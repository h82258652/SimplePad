using System.Windows;
using System.Windows.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class PasteMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(PasteMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public PasteMenuItem()
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
        PasteMenuItem self = (PasteMenuItem)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;
        self.PasteCommand.TextBox = textBox;
    }
}