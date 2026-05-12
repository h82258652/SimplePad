using System;
using System.Threading.Tasks;

namespace SimplePad.Search;

internal sealed class WPFSearchSettings : ISearchSettings
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
        IsMatchCase = Properties.Settings.Default.IsMatchCase;
        IsWrapAround = Properties.Settings.Default.IsWrapAround;
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Properties.Settings.Default.IsMatchCase = IsMatchCase;
        Properties.Settings.Default.IsWrapAround = IsWrapAround;
        Properties.Settings.Default.Save();
        return Task.CompletedTask;
    }
}
