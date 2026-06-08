using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public partial class SearchControl : UserControl
{
    private readonly SearchViewState _searchViewState;

    public SearchControl()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();
    }
}