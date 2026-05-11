using System;
using System.Threading.Tasks;

namespace SimplePad.StatusBar;

internal sealed class WPFStatusBarSettings : IStatusBarSettings
{
    private bool _isStatusBarVisible = true;

    public event EventHandler<bool>? IsStatusBarVisibleChanged;

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

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}