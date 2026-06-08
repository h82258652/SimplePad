using System;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class AvaloniaSearchSettings : ISearchSettings
{
    private bool _isMatchCase;
    private bool _isWrapAround = true;

    public event EventHandler<bool>? IsMatchCaseChanged;

    public event EventHandler<bool>? IsWrapAroundChanged;

    public bool IsMatchCase
    {
        get => _isMatchCase;
        set
        {
            if (_isMatchCase != value)
            {
                _isMatchCase = value;
                IsMatchCaseChanged?.Invoke(this, value);
            }
        }
    }

    public bool IsWrapAround
    {
        get => _isWrapAround;
        set
        {
            if (_isWrapAround != value)
            {
                _isWrapAround = value;
                IsWrapAroundChanged?.Invoke(this, value);
            }
        }
    }

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}