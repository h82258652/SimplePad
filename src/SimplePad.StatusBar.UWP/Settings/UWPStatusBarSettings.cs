using SimplePad.StatusBar.Settings;
using System;

namespace SimplePad.StatusBar.UWP.Settings;

public sealed class UWPStatusBarSettings : IStatusBarSettings
{
    private bool _isStatusBarVisible = false;

    public bool IsStatusBarVisible
    {
        get => _isStatusBarVisible;
        set
        {
            if (_isStatusBarVisible != value)
            {
                _isStatusBarVisible = value;
                IsStatusBarVisibleChanged?.Invoke(this, value);
            }
        }
    }

    public event EventHandler<bool>? IsStatusBarVisibleChanged;
}
