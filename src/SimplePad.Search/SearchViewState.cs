using System;

namespace SimplePad.Search;

public sealed class SearchViewState
{
    private bool _isVisible;

    internal SearchViewState()
    {
    }

    public event EventHandler<bool>? IsVisibleChanged;

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