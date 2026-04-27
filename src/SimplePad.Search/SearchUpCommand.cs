using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed class SearchUpCommand : ICommand
{
    private readonly ISearchSettings _searchSettings;
    private readonly SearchViewState _searchViewState;

    public SearchUpCommand()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
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
            //SearchTextNotFoundDialog searchTextNotFoundDialog = new(searchText);
            //await searchTextNotFoundDialog.ShowAsync();
        }
    }
}