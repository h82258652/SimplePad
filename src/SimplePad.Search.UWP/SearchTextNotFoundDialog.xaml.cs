using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Themes;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class SearchTextNotFoundDialog : ContentDialog
{
    public SearchTextNotFoundDialog(string searchText)
    {
        IThemeSettings themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();
        InitializeComponent();
        RequestedTheme = themeSettings.AppTheme.GetElementTheme();

        SearchText.Text = searchText;
    }
}
