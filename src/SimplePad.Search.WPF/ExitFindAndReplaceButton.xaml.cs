using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public partial class ExitFindAndReplaceButton : Button
{
    private readonly SearchViewState _searchViewState;

    public ExitFindAndReplaceButton()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _searchViewState.IsVisible = false;
    }
}
