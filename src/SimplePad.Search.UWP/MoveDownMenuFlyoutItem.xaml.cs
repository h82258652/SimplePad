using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class MoveDownMenuFlyoutItem : MenuFlyoutItem
{
    public MoveDownMenuFlyoutItem()
    {
        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchDownCommand searchDownCommand = new();
        searchDownCommand.Execute(null);
    }
}