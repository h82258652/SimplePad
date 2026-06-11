using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class MoveUpMenuItem : MenuFlyoutItem
{
    public MoveUpMenuItem()
    {
        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}