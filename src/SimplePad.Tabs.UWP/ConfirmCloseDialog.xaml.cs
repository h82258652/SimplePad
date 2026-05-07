using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Themes;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Tabs;

public sealed partial class ConfirmCloseDialog : ContentDialog
{
    public ConfirmCloseDialog(Tab tab)
    {
        IThemeSettings themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();
        InitializeComponent();
        RequestedTheme = themeSettings.AppTheme.GetElementTheme();

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