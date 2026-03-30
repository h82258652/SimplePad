using System;
using System.Threading.Tasks;
using SimplePad.Core.UWP.Settings;
using SimplePad.Themes.Settings;

namespace SimplePad.Themes.UWP.Settings;

public sealed class UWPThemeSettings : AppSettingsBase, IThemeSettings
{
    private AppTheme _appTheme = AppTheme.Default;

    public event EventHandler<AppTheme>? AppThemeChanged;

    public AppTheme AppTheme
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public override Task LoadAsync()
    {
        throw new NotImplementedException();
    }

    public override Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}
