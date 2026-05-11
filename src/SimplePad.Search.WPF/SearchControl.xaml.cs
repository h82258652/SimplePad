using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed partial class SearchControl : UserControl
{
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();

        Visibility = _searchViewState.IsVisible ? Visibility.Visible : Visibility.Collapsed;

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void Hide()
    {
        Visibility = Visibility.Collapsed;
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
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

    private void Show()
    {
        Visibility = Visibility.Visible;
    }
}