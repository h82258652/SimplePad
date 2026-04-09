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

        _searchViewState.IsVisibleChanged += OnSearchViewStateIsVisibleChanged;
    }

    private void OnExitButtonClick(object sender, RoutedEventArgs e)
    {
    }

    private void OnSearchViewStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnXoooo(object sender, RoutedEventArgs e)
    {
    }

    private void Onyyyyy(object sender, RoutedEventArgs e)
    {
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