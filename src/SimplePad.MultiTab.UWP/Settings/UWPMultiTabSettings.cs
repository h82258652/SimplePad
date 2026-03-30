using System;
using System.Threading.Tasks;
using SimplePad.MultiTab.Settings;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.MultiTab.UWP.Settings;

public sealed class UWPMultiTabSettings : IMultiTabSettings
{
    private OpenFileBehavior _openFileBehavior = OpenFileBehavior.NewTab;

    public event EventHandler<OpenFileBehavior>? OpenFileBehaviorChanged;

    public OpenFileBehavior OpenFileBehavior
    {
        get => _openFileBehavior;
        set
        {
            if (_openFileBehavior != value)
            {
                _openFileBehavior = value;
                OpenFileBehaviorChanged?.Invoke(this, value);
            }
        }
    }

    public Task LoadAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        if (settingsValue.TryGetValue(nameof(OpenFileBehavior), out object? openFileBehavior))
        {
            OpenFileBehavior = (OpenFileBehavior)openFileBehavior;
        }

        return Task.CompletedTask;
    }

    public Task SaveAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        settingsValue[nameof(OpenFileBehavior)] = (int)OpenFileBehavior;

        return Task.CompletedTask;
    }
}