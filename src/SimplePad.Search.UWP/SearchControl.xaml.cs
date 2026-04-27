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
        UpdateSearchTextBox();

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;
    }

    private void OnSearchTextBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        _searchViewState.SearchText = SearchTextBox.Text;
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateGgggg();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateSearchTextBox();
    }

    private void UpdateGgggg()
    {
        if (_searchViewState.IsReplaceMode)
        {
            ReplaceModePanel.Visibility = Visibility.Visible;
        }
        else
        {
            ReplaceModePanel.Visibility = Visibility.Collapsed;
        }
    }

    private void UpdateSearchTextBox()
    {
        SearchTextBox.Text = _searchViewState.SearchText;
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
}