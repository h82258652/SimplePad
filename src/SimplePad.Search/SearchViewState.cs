using System;
using SimplePad.Editor;

namespace SimplePad.Search;

public sealed class SearchViewState
{
    private bool _isReplaceMode;
    private bool _isVisible;
    private string _searchText = string.Empty;

    internal SearchViewState()
    {
    }

    public event EventHandler<bool>? IsReplaceModeChanged;

    public event EventHandler<bool>? IsVisibleChanged;

    public event EventHandler<string>? SearchTextChanged;

    public bool IsReplaceMode
    {
        get => _isReplaceMode;
        set
        {
            if (_isReplaceMode != value)
            {
                _isReplaceMode = value;
                IsReplaceModeChanged?.Invoke(this, value);
            }
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                IsVisibleChanged?.Invoke(this, value);
            }
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText != value)
            {
                _searchText = value;
                SearchTextChanged?.Invoke(this, value);
            }
        }
    }

    public IAppTextBox? TextBox { get; set; }
}