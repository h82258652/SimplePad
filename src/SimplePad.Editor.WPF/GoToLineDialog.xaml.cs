using System.Windows;

namespace SimplePad.Editor;

public sealed partial class GoToLineDialog : Window
{
    internal GoToLineDialog(int currentLine, int maxLine)
    {
        InitializeComponent();

        LineNumberBox.Value = currentLine;
        LineNumberBox.Maximum = maxLine;
    }

    internal int LineNumber { get; private set; }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnGoToButtonClick(object sender, RoutedEventArgs e)
    {
        LineNumber = (int)(LineNumberBox.Value ?? 0);

        DialogResult = true;
        Close();
    }
}