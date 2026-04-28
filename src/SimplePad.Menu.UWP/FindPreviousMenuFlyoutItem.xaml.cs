using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FindPreviousMenuFlyoutItem : MenuFlyoutItem
{
    private readonly SearchViewState _searchViewState;

    public FindPreviousMenuFlyoutItem()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        string searchText = _searchViewState.SearchText;
        if (string.IsNullOrEmpty(searchText))
        {
            _searchViewState.IsVisible = true;
            _searchViewState.IsReplaceMode = false;
            return;
        }

        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}