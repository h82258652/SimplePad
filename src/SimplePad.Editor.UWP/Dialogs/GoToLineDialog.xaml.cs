using Windows.UI.Xaml.Controls;

namespace SimplePad.Editor.UWP.Dialogs;

public sealed partial class GoToLineDialog : ContentDialog
{
    public GoToLineDialog(int currentLine, int maxLine)
    {
        InitializeComponent();

        LineNumberBox.Value = currentLine;
        LineNumberBox.Maximum = maxLine;
    }

    public int LineNumber { get; private set; }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        LineNumber = (int)LineNumberBox.Value;
    }
}