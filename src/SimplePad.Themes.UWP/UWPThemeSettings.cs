using System;
using System.Threading.Tasks;
using SimplePad.Core.Settings;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.Themes;

internal sealed class UWPThemeSettings : AppSettingsBase, IThemeSettings
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
                AppThemeChanged?.Invoke(this, value);
            }
        }
    }

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = GetSettings();
        if (settingsValue.TryGetValue(nameof(AppTheme), out object? appTheme))
        {
            AppTheme = (AppTheme)appTheme;
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = GetSettings();
        settingsValue[nameof(AppTheme)] = (int)AppTheme;

        return Task.CompletedTask;
    }

    private static IPropertySet GetSettings()
    {
        return ApplicationData.Current.LocalSettings.CreateContainer("Themes", ApplicationDataCreateDisposition.Always).Values;
    }
}