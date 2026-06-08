using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Themes;

namespace SimplePad.Editor;

internal sealed partial class GoToLineDialog : ContentDialog
{
    internal GoToLineDialog(int currentLine, int maxLine)
    {
        IThemeSettings themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();
        InitializeComponent();
        RequestedTheme = themeSettings.AppTheme.GetElementTheme();

        LineNumberBox.Value = currentLine;
        LineNumberBox.Maximum = maxLine;
    }

    internal int LineNumber { get; private set; }

    private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        LineNumber = (int)LineNumberBox.Value;
    }
}