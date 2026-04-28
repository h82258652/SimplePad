using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FindNextMenuFlyoutItem : MenuFlyoutItem
{
    private readonly SearchViewState _searchViewState;

    public FindNextMenuFlyoutItem()
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

        SearchDownCommand searchDownCommand = new();
        searchDownCommand.Execute(null);
    }
}