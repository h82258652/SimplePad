using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class MoveDownMenuItem : MenuFlyoutItem
{
    public MoveDownMenuItem()
    {
        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchDownCommand searchDownCommand = new();
        searchDownCommand.Execute(null);
    }
}