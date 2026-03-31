using SimplePad.Core.UWP.Settings;
using SimplePad.StatusBar.Settings;
using System;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.StatusBar.UWP.Settings;

public sealed class UWPStatusBarSettings : AppSettingsBase, IStatusBarSettings
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
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        if (settingsValue.TryGetValue(nameof(IsStatusBarVisible), out object? isStatusBarVisible))
        {
            IsStatusBarVisible = (bool)isStatusBarVisible;
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        settingsValue[nameof(IsStatusBarVisible)] = IsStatusBarVisible;

        return Task.CompletedTask;
    }
}
