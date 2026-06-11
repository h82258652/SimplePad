using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed class SearchControlExpander : Expander
{
    private readonly SearchViewState _searchViewState;

    public SearchControlExpander()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        UpdateIsExpanded();

        Expanded += OnExpanded;
        Collapsed += OnCollapsed;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
    }

    private void OnCollapsed(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsReplaceMode = IsExpanded;
    }

    private void OnExpanded(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsReplaceMode = IsExpanded;
    }

    private void OnSearchViewStateIsReplaceModeChanged(object? sender, bool e)
    {
        UpdateIsExpanded();
    }

    private void UpdateIsExpanded()
    {
        IsExpanded = _searchViewState.IsReplaceMode;
    }
}