using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;

namespace SimplePad.Menu;

public sealed class FindCommand : ICommand
{
    private readonly SearchViewState _searchViewState;

    public FindCommand()
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
        _searchViewState.IsVisible = true;
        _searchViewState.IsReplaceMode = false;

        if (_searchViewState.TextBox is { } textBox)
        {
            string selectedText = textBox.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                _searchViewState.SearchText = selectedText;
            }
        }
    }
}