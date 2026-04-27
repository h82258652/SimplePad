using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

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
        Regex regex = new(Regex.Escape(searchText), regexOptions);
        string text = textBox.Text;

        var r = regex.Replace(text, "");

        //textBox.Text = r;
        textBox.SelectionStart = 0;
    }
}