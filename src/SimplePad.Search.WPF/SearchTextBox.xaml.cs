using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Wpf.Ui.Controls;

namespace SimplePad.Search;

public partial class SearchTextBox : AutoSuggestBox
{
    private readonly SearchViewState _searchViewState;

    public SearchTextBox()
    {
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        InitializeComponent();

        UpdateTextBoxText();

        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateTextBoxText();
    }

    private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        _searchViewState.SearchText = Text;
    }

    private void UpdateTextBoxText()
    {
        Text = _searchViewState.SearchText;
    }
}
