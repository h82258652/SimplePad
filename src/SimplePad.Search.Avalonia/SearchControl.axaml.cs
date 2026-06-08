using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;

namespace SimplePad.Search;

public partial class SearchControl : UserControl
{
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();

        IsVisible = _searchViewState.IsVisible;

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void Hide()
    {
        throw new NotImplementedException();
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void Show()
    {
        throw new NotImplementedException();
    }

    private void UpdateVisibility()
    {
        if (_searchViewState.IsVisible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}