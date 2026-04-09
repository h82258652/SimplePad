using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ReplaceMenuFlyoutItem : MenuFlyoutItem
{
    private readonly SearchViewState _searchViewState;

    public ReplaceMenuFlyoutItem()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsVisible = true;
        _searchViewState.IsReplaceMode = true;
    }
}