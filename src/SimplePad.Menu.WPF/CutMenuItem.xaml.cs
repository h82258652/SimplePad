using System.Windows;
using System.Windows.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class CutMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(CutMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public CutMenuItem()
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
        CutMenuItem self = (CutMenuItem)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;
        self.CutCommand.TextBox = textBox;
    }
}