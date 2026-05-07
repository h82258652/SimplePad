using System;
using System.Threading.Tasks;

namespace SimplePad.Themes;

internal sealed class WPFThemeSettings : IThemeSettings
{
    public AppTheme AppTheme
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public event EventHandler<AppTheme>? AppThemeChanged;

    public Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
