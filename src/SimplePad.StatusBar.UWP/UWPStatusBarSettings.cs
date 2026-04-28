using System;
using System.Threading.Tasks;
using SimplePad.Core.Settings;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.StatusBar;

internal sealed class UWPStatusBarSettings : AppSettingsBase, IStatusBarSettings
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

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = GetSettings();
        if (settingsValue.TryGetValue(nameof(IsStatusBarVisible), out object? isStatusBarVisible))
        {
            IsStatusBarVisible = (bool)isStatusBarVisible;
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = GetSettings();
        settingsValue[nameof(IsStatusBarVisible)] = IsStatusBarVisible;

        return Task.CompletedTask;
    }

    private static IPropertySet GetSettings()
    {
        return ApplicationData.Current.LocalSettings.CreateContainer("StatusBar", ApplicationDataCreateDisposition.Always).Values;
    }
}
