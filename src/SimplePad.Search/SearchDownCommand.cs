using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SimplePad.Search;

public sealed class SearchDownCommand : ICommand
{
    private readonly ISearchDialogService _searchDialogService;
    private readonly ISearchNotificationService _searchNotificationService;
    private readonly ISearchSettings _searchSettings;
    private readonly SearchViewState _searchViewState;
    private bool _executable = true;

    public SearchDownCommand()
    {
        _searchSettings = ServiceLocator.Current.GetRequiredService<ISearchSettings>();
        _searchViewState = ServiceLocator.Current.GetRequiredService<SearchViewState>();
        _searchNotificationService = ServiceLocator.Current.GetRequiredService<ISearchNotificationService>();
        _searchDialogService = ServiceLocator.Current.GetRequiredService<ISearchDialogService>();

        UpdateExecutable();

        _searchViewState.SearchTextChanged += OnSearchViewStateSearchTextChanged;
    }

    public event EventHandler? CanExecuteChanged;

    private bool Executable
    {
        get => _executable;
        set
        {
            if (_executable != value)
            {
                _executable = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool CanExecute(object? parameter)
    {
        return Executable;
    }

    public async void Execute(object? parameter)
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
        int selectionStart = textBox.SelectionStart;
        int selectionLength = textBox.SelectionLength;

        Match match = regex.Match(text, selectionStart + selectionLength);
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
            _searchNotificationService.ShowFindNextFromTopNotification();
        }
        else
        {
            await _searchDialogService.ShowSearchTextNotFoundDialogAsync(searchText);
        }
    }

    private void OnSearchViewStateSearchTextChanged(object? sender, string e)
    {
        UpdateExecutable();
    }

    private void UpdateExecutable()
    {
        Executable = !string.IsNullOrEmpty(_searchViewState.SearchText);
    }
}