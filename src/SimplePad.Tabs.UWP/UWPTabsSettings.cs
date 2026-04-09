using System;
using System.Threading.Tasks;
using SimplePad.Core.Settings;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace SimplePad.Tabs;

public sealed class UWPTabsSettings : AppSettingsBase, ITabsSettings
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

    public override Task LoadAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        if (settingsValue.TryGetValue(nameof(OpenFileBehavior), out object? openFileBehavior))
        {
            OpenFileBehavior = OpenFileBehavior.FromValue((int)openFileBehavior);
        }

        return Task.CompletedTask;
    }

    public override Task SaveAsync()
    {
        IPropertySet settingsValue = ApplicationData.Current.LocalSettings.Values;
        settingsValue[nameof(OpenFileBehavior)] = OpenFileBehavior.Value;

        return Task.CompletedTask;
    }
}
