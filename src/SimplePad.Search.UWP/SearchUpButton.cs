using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Search;

internal sealed partial class SearchUpButton : Button
{
    private readonly ISearchSettings _searchSettings;
    private readonly SearchViewState _searchViewState;

    public SearchUpButton()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();

        DefaultStyleKey = typeof(SearchUpButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Search.UWP/SearchUpButton.xaml");

        Click += OnClick;
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (_searchViewState.TextBox is not { } textBox)
        {
            return;
        }

        string searchText = _searchViewState.SearchText;
        if (string.IsNullOrEmpty(searchText))
        {
            return;
        }

        RegexOptions regexOptions = SearchSettingsHelper.GetRegexOptions(_searchSettings);
        regexOptions |= RegexOptions.RightToLeft;
        Regex regex = new(Regex.Escape(searchText), regexOptions);
        string text = textBox.Text;
        int selectionStart = textBox.SelectionStart;

        Match match = regex.Match(text, selectionStart);
        if (match.Success)
        {
            textBox.SelectionStart = match.Index;
            textBox.SelectionLength = match.Length;
            return;
        }

        match = regex.Match(text);
        if (match.Success)
        {
            textBox.SelectionStart = match.Index;
            textBox.SelectionLength = match.Length;
            // TODO Show tooltip
        }
        else
        {
            SearchTextNotFoundDialog searchTextNotFoundDialog = new(searchText);
            await searchTextNotFoundDialog.ShowAsync();
        }
    }
}