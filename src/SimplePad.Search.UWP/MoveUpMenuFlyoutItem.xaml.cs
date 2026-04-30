using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class MoveUpMenuFlyoutItem : MenuFlyoutItem
{
    public MoveUpMenuFlyoutItem()
    {
        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}