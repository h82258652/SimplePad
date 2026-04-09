using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FindMenuFlyoutItem : MenuFlyoutItem
{
    private readonly SearchViewState _searchViewState;

    public FindMenuFlyoutItem()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsVisible = true;
    }
}