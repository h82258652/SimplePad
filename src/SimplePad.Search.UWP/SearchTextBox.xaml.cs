using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

public sealed partial class SearchTextBox : UserControl
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

    private void OnTextBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        _searchViewState.SearchText = TextBox.Text;
    }

    private void UpdateTextBoxText()
    {
        TextBox.Text = _searchViewState.SearchText;
    }
}