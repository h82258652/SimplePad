using System.Windows;
using System.Windows.Controls;
using SimplePad.Editor;

namespace SimplePad.Menu;

public sealed partial class CopyMenuItem : MenuItem
{
    public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
        nameof(TextBox),
        typeof(IAppTextBox),
        typeof(CopyMenuItem),
        new PropertyMetadata(null, OnTextBoxChanged));

    public CopyMenuItem()
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
        CopyMenuItem self = (CopyMenuItem)d;
        IAppTextBox? textBox = (IAppTextBox?)e.NewValue;
        self.CopyCommand.TextBox = textBox;
    }
}