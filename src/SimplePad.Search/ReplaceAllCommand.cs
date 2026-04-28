using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SimplePad.Search;

public sealed class ReplaceAllCommand : ICommand
{
    private readonly ISearchSettings _searchSettings;
    private readonly SearchViewState _searchViewState;

    public ReplaceAllCommand()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
    }

    public event EventHandler? CanExecuteChanged
    {
        add
        {
        }
        remove
        {
        }
    }

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
        Regex regex = new(Regex.Escape(searchText), regexOptions);
        string text = textBox.Text;
        string replaceText = _searchViewState.ReplaceText;

        string replacedText = regex.Replace(text, replaceText);

        textBox.Text = replacedText;
        textBox.SelectionStart = 0;
    }
}