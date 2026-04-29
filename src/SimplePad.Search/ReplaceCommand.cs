using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Search;

public sealed class ReplaceCommand : ICommand
{
    private readonly SearchViewState _searchViewState;

    public ReplaceCommand()
    {
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

        string selectedText = textBox.SelectedText;
        if (selectedText != searchText)
        {
            SearchDownCommand searchDownCommand = new();
            searchDownCommand.Execute(null);
            return;
        }

        textBox.SelectedText = _searchViewState.ReplaceText;
    }
}