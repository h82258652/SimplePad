using System;

namespace SimplePad.Search;

public sealed class SearchViewState
{
    private bool _isReplaceMode;
    private bool _isVisible;

    internal SearchViewState()
    {
    }

    public event EventHandler<bool>? IsReplaceModeChanged;

    public event EventHandler<bool>? IsVisibleChanged;

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
}