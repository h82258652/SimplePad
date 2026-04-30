using System;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Search;

namespace SimplePad.Menu;

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
        _searchViewState.IsVisible = true;
        _searchViewState.IsReplaceMode = true;

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