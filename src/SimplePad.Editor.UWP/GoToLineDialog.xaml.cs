using Windows.UI.Xaml.Controls;

namespace SimplePad.Editor;

internal sealed partial class GoToLineDialog : ContentDialog
{
    internal GoToLineDialog(int currentLine, int maxLine)
    {
        InitializeComponent();

        LineNumberBox.Value = currentLine;
        LineNumberBox.Maximum = maxLine;
    }

    internal int LineNumber { get; private set; }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        LineNumber = (int)LineNumberBox.Value;
    }
}
