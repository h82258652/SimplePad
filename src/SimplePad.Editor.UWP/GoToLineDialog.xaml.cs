using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Themes;
using Windows.UI.Xaml.Controls;

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
