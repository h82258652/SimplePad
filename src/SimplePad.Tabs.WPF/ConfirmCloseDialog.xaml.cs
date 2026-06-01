using System;
using System.Windows;

namespace SimplePad.Tabs;

public sealed partial class ConfirmCloseDialog : Window
{
    public ConfirmCloseDialog(Tab tab)
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

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ConfirmCloseResult.Cancel;
        DialogResult = false;
    }

    private void OnDontSaveButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ConfirmCloseResult.Discard;
        DialogResult = true;
    }

    private void OnSaveButtonClick(object sender, RoutedEventArgs e)
    {
        Result = ConfirmCloseResult.Save;
        DialogResult = true;
    }
}