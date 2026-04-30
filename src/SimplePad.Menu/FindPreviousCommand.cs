using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;

namespace SimplePad.Menu;

public sealed class FindPreviousCommand : ICommand
{
    private readonly SearchViewState _searchViewState;

    public FindPreviousCommand()
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
        string searchText = _searchViewState.SearchText;
        if (string.IsNullOrEmpty(searchText))
        {
            _searchViewState.IsVisible = true;
            _searchViewState.IsReplaceMode = false;
            return;
        }

        SearchUpCommand searchUpCommand = new();
        searchUpCommand.Execute(null);
    }
}