using Windows.UI.Xaml.Controls;

namespace SimplePad.Tabs;

public sealed partial class ConfirmCloseDialog : ContentDialog
{
    public ConfirmCloseDialog()
    {
        InitializeComponent();
    }

    public ConfirmCloseResult? Result { get; private set; }

    private void OnCloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        Result = ConfirmCloseResult.Cancel;
    }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        Result = ConfirmCloseResult.Save;
    }

    private void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        Result = ConfirmCloseResult.Discard;
    }
}