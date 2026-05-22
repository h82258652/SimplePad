using System;
using System.Threading.Tasks;

namespace SimplePad.Themes;

internal sealed class WPFThemeSettings : IThemeSettings
{
    private AppTheme _appTheme = AppTheme.Default;

    public event EventHandler<AppTheme>? AppThemeChanged;

    public AppTheme AppTheme
    {
        get => _appTheme;
        set
        {
            if (_appTheme != value)
            {
                _appTheme = value;
                AppThemeChanged?.Invoke(this, _appTheme);
            }
        }
    }

    public Task LoadAsync()
    {
        AppTheme = (AppTheme)Properties.Settings.Default.AppTheme;
        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        Properties.Settings.Default.AppTheme = (int)AppTheme;
        Properties.Settings.Default.Save();
        return Task.CompletedTask;
    }
}