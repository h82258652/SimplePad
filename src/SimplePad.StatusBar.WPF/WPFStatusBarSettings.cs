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
        IsStatusBarVisible = Properties.Settings.Default.IsStatusBarVisible;
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Properties.Settings.Default.IsStatusBarVisible = IsStatusBarVisible;
        Properties.Settings.Default.Save();
        return Task.CompletedTask;
    }
}