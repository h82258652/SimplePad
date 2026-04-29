using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed partial class SearchControlExpander : Expander
{
    private readonly SearchViewState _searchViewState;

    public SearchControlExpander()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(SearchControlExpander);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/SearchControlExpander.xaml");

        Expanding += OnExpanding;
        Collapsed += OnCollapsed;
        _searchViewState.IsReplaceModeChanged += OnSearchViewStateIsReplaceModeChanged;
    }

    private void OnCollapsed(Expander sender, ExpanderCollapsedEventArgs args)
    {
        _searchViewState.IsReplaceMode = IsExpanded;
    }

    private void OnExpanding(Expander sender, ExpanderExpandingEventArgs args)
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