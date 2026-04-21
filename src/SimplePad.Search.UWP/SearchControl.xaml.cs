using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class SearchControl : UserControl
{
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();

        UpdateVisibility();
        UpdateGgggg();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;

        UpdateSearchTextBox();
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateSearchTextBox();
    }

    private void UpdateSearchTextBox()
    {
        SearchTextBox.Text = _searchViewState.SearchText;
    }

    private void UpdateGgggg()
    {
        if (_searchViewState.IsReplaceMode)
        {
            // TODO
        }
        else
        {
            // TODO
        }
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateGgggg();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        if (_searchViewState.IsVisible)
        {
            Visibility = Visibility.Visible;
        }
        else
        {
            Visibility = Visibility.Collapsed;
        }
    }

    private void OnSearchTextBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        _searchViewState.SearchText = SearchTextBox.Text;
    }
}