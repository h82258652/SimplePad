using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class SearchTextNotFoundDialog : ContentDialog
{
    public SearchTextNotFoundDialog(string searchText)
    {
        InitializeComponent();

        SearchText.Text = searchText;
    }
}
