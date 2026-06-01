using Wpf.Ui.Controls;

namespace SimplePad.Editor;

public sealed partial class GoToLineDialog : ContentDialog
{
    internal GoToLineDialog(int currentLine, int maxLine)
    {
        InitializeComponent();

        LineNumberBox.Value = currentLine;
        LineNumberBox.Maximum = maxLine;
    }

    internal int LineNumber { get; private set; }

    private void ContentDialog_ButtonClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (args.Button == ContentDialogButton.Primary)
        {
            LineNumber = (int)(LineNumberBox.Value ?? 0);
        }
    }
}
