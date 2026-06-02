using System;
using Wpf.Ui.Controls;

namespace SimplePad.Tabs;

public sealed partial class ConfirmCloseDialog : ContentDialog
{
    internal ConfirmCloseDialog(Tab tab)
    {
        InitializeComponent();
        FileNameText.Text = GetFileName(tab);
    }

    public ConfirmCloseResult? Result { get; private set; }

    private static string GetFileName(Tab tab)
    {
        if (tab.File is { } file)
        {
            return file.FileName;
        }

        string title = tab.Title;
        return title[..Math.Min(title.Length, 35)] + ".txt";
    }

    private void OnButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (args.Button == ContentDialogButton.Primary)
        {
            Result = ConfirmCloseResult.Save;
        }
        else if (args.Button == ContentDialogButton.Secondary)
        {
            Result = ConfirmCloseResult.Discard;
        }
        else if (args.Button == ContentDialogButton.Close)
        {
            Result = ConfirmCloseResult.Cancel;
        }
    }
}
